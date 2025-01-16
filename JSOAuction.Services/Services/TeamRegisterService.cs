using AutoMapper;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Upload;
using JSOAuction.Data.Contexts;
using JSOAuction.Data.Infrastructure;
using JSOAuction.Domain.Entities.TeamRegister;
using JSOAuction.Domain.Entities.Tournament;
using JSOAuction.Services.Entities.PlayersDetailsByTeam;
using JSOAuction.Services.Entities.Team;
using JSOAuction.Services.Entities.Tournament;
using JSOAuction.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Data;
using System.Diagnostics;

namespace JSOAuction.Services.Services
{
    public class TeamRegisterService : ITeamRegisterService
    {
        private readonly IUnitOfWork<ReadOnlyApplicationDbContext> _readOnlyUnitOfWork;
        private readonly IUnitOfWork<ReadWriteApplicationDbContext> _readWriteUnitOfWork;
        private readonly ReadWriteApplicationDbContext _readWriteUnitOfWorkSP;
        private readonly IUnitOfWork<MasterDbContext> _masterDBContext;
        private readonly IMapper _mapper;
        public TeamRegisterService(IUnitOfWork<ReadOnlyApplicationDbContext> readOnlyUnitOfWork,
           IUnitOfWork<MasterDbContext> masterDBContext, IMapper mapper,
           IUnitOfWork<ReadWriteApplicationDbContext> readWriteUnitOfWork,
           ReadWriteApplicationDbContext readWriteUnitOfWorkSP)
        {
            _readOnlyUnitOfWork = readOnlyUnitOfWork;
            _masterDBContext = masterDBContext;
            _readWriteUnitOfWork = readWriteUnitOfWork;
            _mapper = mapper;
            _readWriteUnitOfWorkSP = readWriteUnitOfWorkSP;
        }

        public async Task<List<TeamRegister>> GetAllTeamDetails(TeamIdNameDto teamIdNameDto)
        {
            IEnumerable<TeamRegister> teams = new List<TeamRegister>();
            _readWriteUnitOfWorkSP.LoadStoredProc("GetAllRegisteredTeams")
                .WithSqlParam("@AuctionId", teamIdNameDto.AuctionId)
                .ExecuteStoredProc((handler) =>
                {
                    teams = handler.ReadToList<TeamRegister>();
                });
            if (teams == null || !teams.Any())
            {
                throw new Exception("No teams found");
            }
            return teams.ToList();
        }

        public async Task<List<PlayersDetailsByTeamResponseModel>> GetPlayerDetailsByTeam(PlayerDetailsTeamWiseDto request)
        {
            IEnumerable<PlayersDetailsTeamWiseModel> playerDetails = new List<PlayersDetailsTeamWiseModel>();
            _readWriteUnitOfWorkSP.LoadStoredProc("GetPlayersDetailsByTeam")
                .WithSqlParam("@AuctionId", request.AuctionId)
                .ExecuteStoredProc((handler) =>
                {
                    playerDetails = handler.ReadToList<PlayersDetailsTeamWiseModel>();
                });

            var teamWisePlayerDetails = new List<PlayersDetailsByTeamResponseModel>();

            foreach (var teamGroup in playerDetails.GroupBy(pd => new { pd.TeamId, pd.TeamName, pd.TeamLogo }))
            {
                var teamModel = new PlayersDetailsByTeamResponseModel
                {
                    TeamId = teamGroup.Key.TeamId,
                    TeamName = teamGroup.Key.TeamName,
                    TeamLogo = teamGroup.Key.TeamLogo,
                    PlayerDetails = teamGroup.ToList()
                };

                teamWisePlayerDetails.Add(teamModel);
            }
            return teamWisePlayerDetails;

        }
        public async Task<List<TeamIdNameResponseModel>> GetTeamIdNameModel(TeamIdNameDto request)
        {
            IEnumerable<TeamIdNameResponseModel> teamDetails = new List<TeamIdNameResponseModel>();
            _readWriteUnitOfWorkSP.LoadStoredProc("GetTeamIdNameDetails")
                .WithSqlParam("@AuctionId", request.AuctionId)
                .WithSqlParam("@TournamentId", request.TournamentId)
                .ExecuteStoredProc((handler) =>
                {
                    teamDetails = handler.ReadToList<TeamIdNameResponseModel>();
                });

            if (teamDetails == null || !teamDetails.Any())
            {
                throw new Exception("No teams found");
            }

            var currentYear = DateTime.UtcNow.Year;

            var teams = await _readWriteUnitOfWork.TeamRegisterRepository.GetAllAsync(x => x.TournamentId == request.TournamentId && x.IsDeleted == false);
            var filteredTeams = teams.Where(t => t.FoundedYear < currentYear).Select(t => t.TeamId).ToList();
            var filteredTeamDetails = teamDetails.Where(td => filteredTeams.Contains(td.TeamId)).ToList();

            return filteredTeamDetails;
        }

        public async Task<object> SaveTeam(TeamRegisterDto request)
        {
            var existingPlayer = await _readWriteUnitOfWork.TeamRegisterRepository.GetFirstOrDefaultAsync(
                x => x.MobileNumber == request.MobileNumber &&
                     x.TournamentId == request.TournamentId &&
                     x.TeamName == request.TeamName &&
                     x.IsDeleted == false);
            if (existingPlayer != null)
            {
                return ("Mobile number and team name already registered.");
            }

            existingPlayer = await _readWriteUnitOfWork.TeamRegisterRepository.GetFirstOrDefaultAsync(
                x => x.MobileNumber == request.MobileNumber &&
                     x.TournamentId == request.TournamentId &&
                     x.IsDeleted == false);
            if (existingPlayer != null)
            {
                return ("Mobile number already registered.");
            }

            existingPlayer = await _readWriteUnitOfWork.TeamRegisterRepository.GetFirstOrDefaultAsync(
                x => x.TeamName == request.TeamName &&
                     x.TournamentId == request.TournamentId &&
                     x.IsDeleted == false);
            if (existingPlayer != null)
            {
                return ("Team name already registered.");
            }

            var teamData = _readWriteUnitOfWork.TeamRegisterRepository
                .GetAll()
                .Where(x => x.TournamentId == request.TournamentId && x.IsDeleted == false);

            var tournamentData = await _readWriteUnitOfWork.TournamentRegisterRepository
                .GetFirstOrDefaultAsync(x => x.TournamentId == request.TournamentId);

            if (tournamentData.MaxTeams != null && teamData.Count() >= tournamentData.MaxTeams)
            {
                return ("Maximum number of teams already registered.");
            }

            string uploadLogoId = "";
            string webViewLinkLogo = string.Empty;

            if (request.UploadLogoFile != null)
            {
                DriveUploadBasic(request.UploadLogoFile, ref uploadLogoId);

                webViewLinkLogo = "https://drive.google.com/thumbnail?id=" + uploadLogoId + "&sz=w1000";
            }

            var saveTeam = new TeamRegister()
            {
                TeamName = request.TeamName,
                Owner = request.OwnerName,
                MobileNumber = request.MobileNumber,
                Email = request.Email,
                TeamLogo = webViewLinkLogo,
                CoachName = request.CoachName,
                FoundedYear = request.FoundedYear,
                CreatedOn = DateTime.UtcNow,
                IsActive = true,
                IsDeleted = false,
                TournamentId = request.TournamentId,
                TotalBalance = tournamentData.TotalBalance
            };

            await _readWriteUnitOfWork.TeamRegisterRepository.AddAsync(saveTeam);
            await _readWriteUnitOfWork.CommitAsync();

            // Create a response object
            var response = new
            {
                TeamId = saveTeam.TeamId,
                TeamName = saveTeam.TeamName,
                OwnerName = saveTeam.Owner,
                MobileNumber = saveTeam.MobileNumber,
                Email = saveTeam.Email,
                TeamLogo = saveTeam.TeamLogo,
                CoachName = saveTeam.CoachName,
                FoundedYear = saveTeam.FoundedYear,
                TournamentId = saveTeam.TournamentId,
                TotalBalance = saveTeam.TotalBalance
            };

            return response;
        }

        public void DriveUploadBasic(IFormFile file, ref string uploadId)
        {
            string credentialsPath = "credentials.json";
            string jsonCredentials = File.ReadAllText(credentialsPath);
            string folderId = "1CLgzrRw1ntk1laplk5WtZa-Oyh7Afqqo";
            try
            {
                // Initialize the Drive service
                GoogleCredential credential = GoogleCredential.FromJson(jsonCredentials)
                .CreateScoped(DriveService.Scope.Drive);

                var service = new DriveService(new BaseClientService.Initializer
                {
                    HttpClientInitializer = credential,
                    ApplicationName = "Drive API Snippets"
                });

                // Prepare file metadata
                var fileMetadata = new Google.Apis.Drive.v3.Data.File()
                {
                    Name = file.FileName,
                    Parents = new List<string> { folderId }
                };

                FilesResource.CreateMediaUpload request;

                // Read the file content from the IFormFile
                using (var stream = file.OpenReadStream())
                {
                    // Create the upload request
                    request = service.Files.Create(fileMetadata, stream, file.ContentType);

                    // Set fields to retrieve after upload
                    request.Fields = "id";

                    // Upload the file
                    var uploadProgress = request.Upload();

                    // Check if upload is completed successfully
                    if (uploadProgress.Status == UploadStatus.Completed)
                    {
                        var uploadedFile = request.ResponseBody;
                        uploadId = uploadedFile.Id;
                    }
                    else
                    {
                        Debug.WriteLine("File upload failed.");
                    }
                }
            }
            catch (Google.GoogleApiException gae)
            {
                // Handle Google API exceptions
                Debug.WriteLine("Google API Exception: " + gae.Message);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("An error occurred: " + ex.Message);
            }

            //return null;
        }

        public async Task<bool> DeleteTeam(DeleteTeamDto request)
        {
            int isuccess = 1;
            _readWriteUnitOfWorkSP.LoadStoredProc("DeleteTeam")
                .WithSqlParam("@TeamId", request.TeamId)
                .WithSqlParam("@Success", 0, DbType.Int32, ParameterDirection.Output)
                .ExecuteStoredProc((handler) =>
                {
                    isuccess = Convert.ToInt32(handler.GetValue("@Success"));
                });

            if (isuccess > 0)
            {
                return true;
            }
            return false;
        }

        public async Task<object> UpdateTeam(UpdateTeamDto request)
        {
            var existingPlayer = await _readWriteUnitOfWork.TeamRegisterRepository.GetFirstOrDefaultAsync(x => x.MobileNumber == request.MobileNumber && x.TeamId != request.TeamId &&  x.TournamentId == request.TournamentId && x.TeamName == request.TeamName && x.IsDeleted == false);
            if (existingPlayer != null)
            {
                return ("Mobile number and team name already registered.");
            }
            existingPlayer = await _readWriteUnitOfWork.TeamRegisterRepository.GetFirstOrDefaultAsync( x => x.MobileNumber == request.MobileNumber &&  x.TeamId != request.TeamId && x.TournamentId == request.TournamentId && x.IsDeleted == false);
            if (existingPlayer != null)
            {
                return ("Mobile number already registered.");
            }
            existingPlayer = await _readWriteUnitOfWork.TeamRegisterRepository.GetFirstOrDefaultAsync(
                x => x.TeamName == request.TeamName &&
                     x.TeamId != request.TeamId &&
                     x.TournamentId == request.TournamentId &&
                     x.IsDeleted == false);
            if (existingPlayer != null)
            {
                return ("Team name already registered.");
            }
            string uploadLogoId = "";
            string webViewLinkLogo = string.Empty;

            if (request.UploadLogoFile != null)
            {
                DriveUploadBasic(request.UploadLogoFile, ref uploadLogoId);

                webViewLinkLogo = "https://drive.google.com/thumbnail?id=" + uploadLogoId + "&sz=w1000";
            }

            var data = await _readWriteUnitOfWork.TeamRegisterRepository.GetFirstOrDefaultAsync(x => x.TeamId == request.TeamId);

            if (data != null)
            {
                data.TeamName = request.TeamName;
                data.Owner = request.OwnerName;
                data.MobileNumber = request.MobileNumber;
                data.Email = request.Email;
                if (!string.IsNullOrEmpty(webViewLinkLogo))
                {
                    data.TeamLogo = webViewLinkLogo;
                }
                data.CoachName = request.CoachName;
                data.FoundedYear = request.FoundedYear;
                data.CreatedOn = DateTime.UtcNow;
                data.IsActive = true;
                data.IsDeleted = false;
                data.TournamentId = request.TournamentId;
                data.TotalBalance = request.TotalBalance;
                await _readWriteUnitOfWork.CommitAsync();
                return data.TournamentId.Value;
            };
            return 0;
        }

        public async Task<List<TeamDetailsByTournamentResponseModel>> GetTeamDetailsByTournament(GetTeamDetailsByTournamentDto request)
        {
            IEnumerable<TeamDetailsByTournamentResponseModel> teamDetails = new List<TeamDetailsByTournamentResponseModel>();
            _readWriteUnitOfWorkSP.LoadStoredProc("GetTeamDetailsByTournament")
                //.WithSqlParam("@AuctionId", request.AuctionId)
                .WithSqlParam("@TournamentId", request.TournamentId)
                .ExecuteStoredProc((handler) =>
                {
                    teamDetails = handler.ReadToList<TeamDetailsByTournamentResponseModel>();
                });

            //if (teamDetails == null || !teamDetails.Any())
            //{
            //    throw new Exception("No teams found");
            //}
            return teamDetails.ToList();
        }
        public async Task<List<TeamRegister>> GetTeamById(GetTeamDetailsByIdDto request)
        {
            IEnumerable<TeamRegister> teams = new List<TeamRegister>();
            _readWriteUnitOfWorkSP.LoadStoredProc("GetTeamById")
                .WithSqlParam("@Id", request.TeamId)
                .ExecuteStoredProc((handler) =>
                {
                    teams = handler.ReadToList<TeamRegister>();
                });
            if (teams == null || !teams.Any())
            {
                throw new Exception("No teams found");
            }
            return teams.ToList();
        }

    }
}
