using AutoMapper;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Upload;
using JSOAuction.Data.Contexts;
using JSOAuction.Data.Infrastructure;
using JSOAuction.Domain.Entities.Tournament;
using JSOAuction.Services.Entities.Format;
using JSOAuction.Services.Entities.Groups;
using JSOAuction.Services.Entities.Tournament;
using JSOAuction.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient.Server;
using Microsoft.VisualBasic;
using System;
using System.Data;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Reflection.Emit;
using System.Transactions;

namespace JSOAuction.Services.Services
{
    public class TournamentRegisterService : ITournamentRegisterService
    {
        private readonly IUnitOfWork<ReadOnlyApplicationDbContext> _readOnlyUnitOfWork;
        private readonly IUnitOfWork<ReadWriteApplicationDbContext> _readWriteUnitOfWork;
        private readonly ReadWriteApplicationDbContext _readWriteUnitOfWorkSP;
        private readonly IUnitOfWork<MasterDbContext> _masterDBContext;
        private readonly IMapper _mapper;
        public TournamentRegisterService(IUnitOfWork<ReadOnlyApplicationDbContext> readOnlyUnitOfWork,
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

        public async Task<List<TournamentRegister>> GetAllTournamentDetails()
        {
           

            IEnumerable<TournamentRegister> tournament = new List<TournamentRegister>();
            _readWriteUnitOfWorkSP.LoadStoredProc("GetAllTournamentDetails")
                .ExecuteStoredProc((handler) =>
                {
                    tournament = handler.ReadToList<TournamentRegister>();
                });
            // if (tournament == null || !tournament.Any())
            //{
            //     throw new Exception("No tournament found");
            // }
            return tournament.ToList();
        }


        public async Task<int> SaveTournament(TournamentRegisterDto request)
        {
            string uploadBannerId = "";

            string uploadLogoId = "";
            string webViewLinkLogo = string.Empty;
            string webViewLinkBanner = string.Empty;

            if (request.UploadBannerFile != null)
            {
                DriveUploadBasic(request.UploadBannerFile, ref uploadBannerId);

                webViewLinkBanner = "https://drive.google.com/thumbnail?id=" + uploadBannerId + "&sz=w1000";

            }

            if (request.UploadLogoFile != null)
            {
                DriveUploadBasic(request.UploadLogoFile, ref uploadLogoId);

                webViewLinkLogo = "https://drive.google.com/thumbnail?id=" + uploadLogoId + "&sz=w1000";
            }

            var saveTournament = new TournamentRegister()
            {
                TournamentName = request.TournamentName,
                TournamentGuid = Guid.NewGuid(),
                Description = request.Description,
                OrganizerName = request.OrganizerName,
                OrganizerContact = request.OrganizerContact,
                OrganizerEmail = request.OrganizerEmail,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                DueDate = request.DueDate,
                DueTime = request.DueTime,
                AuctionStartTime = request.AuctionStartTime,
                AuctionStartDate = request.AuctionStartDate,
                GroundAddress = request.GroundAddress,
                City = request.City,
                State = request.State,
                Country = request.Country,
                ZipCode = request.ZipCode,
                UploadBanner = webViewLinkBanner,
                UploadLogo = webViewLinkLogo,
                Open = request.Open,
                Corporate = request.Corporate,
                Community = request.Community,
                School = request.School,
                BoxCricket = request.BoxCricket,
                Series = request.Series,
                Other = request.Other,
                BallType = request.BallType,
                PlayerOrderBy = request.PlayerOrderBy,
                Overs = request.Overs,
                Format = request.Format,
                MaxTeams = request.MaxTeams,
                Gender = request.Gender,
                MinPlayer = request.MinPlayer,
                MaxPlayer = request.MaxPlayer,
                PaymentTerms = request.PaymentTerms,
                Amount = request.Amount,
                CreatedOn = DateTime.UtcNow,
                IsActive = true,
                IsDeleted = false,
                BidAmount = request.BidAmount,
                TotalBalance = request.TotalBalance,
                FormatId = request.FormatId
            };
            await _readWriteUnitOfWork.TournamentRegisterRepository.AddAsync(saveTournament);
            await _readWriteUnitOfWork.CommitAsync();
            return saveTournament.TournamentId;
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

        public async Task<bool> DeleteTournament(DeleteTournamentDto request)
        {
            int isuccess = 1;
            _readWriteUnitOfWorkSP.LoadStoredProc("DeleteTournament")
                .WithSqlParam("@TournamentId", request.TournamentId)
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

        public async Task<string> UpdateTournament(UpdateTournamentRegisterDto request)
        {
            int isuccess = 1;

            string success = null;

            string uploadBannerId = "";

            string uploadLogoId = "";
            string webViewLinkLogo = string.Empty;
            string webViewLinkBanner = string.Empty;
            if (request.UploadBannerFile != null)
            {
                DriveUploadBasic(request.UploadBannerFile, ref uploadBannerId);

                 webViewLinkBanner = "https://drive.google.com/thumbnail?id=" + uploadBannerId + "&sz=w1000";

            }

            if(request.UploadLogoFile != null) {
                DriveUploadBasic(request.UploadLogoFile, ref uploadLogoId);

                 webViewLinkLogo = "https://drive.google.com/thumbnail?id=" + uploadLogoId + "&sz=w1000";
            }

            var data = await _readWriteUnitOfWork.TournamentRegisterRepository.GetFirstOrDefaultAsync(x => x.TournamentId == request.TournamentId);
            if (data != null)
            {
                data.TournamentName = request.TournamentName;
                data.Description = request.Description;
                data.OrganizerName = request.OrganizerName;
                data.OrganizerContact = request.OrganizerContact;
                data.OrganizerEmail = request.OrganizerEmail;
                data.StartDate = request.StartDate;
                data.EndDate = request.EndDate;
                data.DueDate = request.DueDate;
                data.DueTime = request.DueTime;
                data.AuctionStartTime = request.AuctionStartTime;
                data.AuctionStartDate = request.AuctionStartDate;
                data.GroundAddress = request.GroundAddress;
                data.City = request.City;
                data.State = request.State;
                data.Country = request.Country;
                data.ZipCode = request.ZipCode;
                if (!string.IsNullOrEmpty(webViewLinkBanner))
                {
                    data.UploadBanner = webViewLinkBanner;
                }

                if (!string.IsNullOrEmpty(webViewLinkLogo))
                {
                    data.UploadLogo = webViewLinkLogo;
                }
                data.Open = request.Open;
                data.Corporate = request.Corporate;
                data.Community = request.Community;
                data.School = request.School;
                data.BoxCricket = request.BoxCricket;
                data.Series = request.Series;
                data.Other = request.Other;
                data.BallType = request.BallType;
                data.PlayerOrderBy = request.PlayerOrderBy;
                data.Overs = request.Overs;
                data.Format = request.Format;
                data.MaxTeams = request.MaxTeams;
                data.Gender = request.Gender;
                data.MinPlayer = request.MinPlayer;
                data.MaxPlayer = request.MaxPlayer; 
                data.PaymentTerms = request.PaymentTerms;
                data.Amount = request.Amount;
                data.UpdatedOn = DateTime.UtcNow;
                data.BidAmount = request.BidAmount;
                data.TotalBalance = request.TotalBalance;
                await _readWriteUnitOfWork.CommitAsync();
                return data.TournamentName; 
            }
            return null;
        }

        public async Task<List<TournamentRegister>> GetTournamentById(GetByTournamentIdDto request)
        {
            IEnumerable<TournamentRegister> tournament = new List<TournamentRegister>();
            _readWriteUnitOfWorkSP.LoadStoredProc("GetTournamentById")
                .WithSqlParam("@Id", request.TournamentId)
                .ExecuteStoredProc((handler) =>
                {
                    tournament = handler.ReadToList<TournamentRegister>();
                });
            if (tournament == null || !tournament.Any())
            {
                throw new Exception("No tournament found");
            }
            return tournament.ToList();
        }

        public async Task<List<TournamentRegister>> GetTournamentAuctionStartStatus(List<int> request)
        {

            if (request == null || !request.Any())
            {
                throw new ArgumentException("Request list cannot be null or empty");
            }

            var tournamentList = new List<TournamentRegister>();

            foreach (var id in request)
            {
                // Retrieve team data for the given tournament
                var teamData =  _readWriteUnitOfWork.TeamRegisterRepository
                    .GetAll()
                    .Where(x => x.TournamentId == id && x.IsDeleted == false)
                    .ToList();

                //Added team data count
                int currentTeamLength = teamData.Count;

                var groupData = _readWriteUnitOfWork.GroupsRepository.GetAll().Where(x => x.TournamentId == id && x.IsDeleted == false);

                // Retrieve player data for the given tournament
                var playerData =  (from player in _readWriteUnitOfWork.PlayerRegisterRepository.GetAll()
                                        join mapping in _readWriteUnitOfWork.AuctionPlayerMappingRepository.GetAll()
                                            on player.PlayerRegisterId equals mapping.PlayerId
                                        where mapping.TournamentId == id && player.IsDeleted == false
                                        select new
                                        {
                                            PlayerId = player.PlayerRegisterId,
                                            PlayerName = player.FirstName,
                                            TournamentId = mapping.TournamentId
                                        }).ToList();

                // Execute the stored procedure to get tournament details
                IEnumerable<TournamentRegister> tournament = new List<TournamentRegister>();
                _readWriteUnitOfWorkSP.LoadStoredProc("GetTournamentById")
               .WithSqlParam("@Id", id)
               .ExecuteStoredProc((handler) =>
               {
                   tournament = handler.ReadToList<TournamentRegister>();
               });

                tournament.First().CurrentTeamLength = currentTeamLength;

                if (teamData.Any() && playerData.Any() && tournament.Any() && groupData.Any())
                {
                    tournament.First().IsStart = true;
                }
                else
                {
                    tournament.First().IsStart = false;
                }

                tournamentList.AddRange(tournament);
            }

            if (!tournamentList.Any())
            {
                throw new Exception("No tournament found");
            }

            return tournamentList;
        }

        public async Task<List<FormatDetailsResponseModel>> GetTournamentFormatById()
        {
            IEnumerable<FormatDetailsResponseModel> format = new List<FormatDetailsResponseModel>();
            try
            {
                _readWriteUnitOfWorkSP.LoadStoredProc("GetFormatList")
                    .ExecuteStoredProc((handler) =>
                    {
                        format = handler.ReadToList<FormatDetailsResponseModel>();
                    });
            }
            catch (Exception ex)
            {
                throw new Exception("Invalid TournamentId provided.", ex);
            }

            var retData = format.ToList();

            return retData;
        }

        public async Task<bool> MatchTournamentLink(MatchTournamentLinkDto request)
        {
                var tournamentRegister = await _readWriteUnitOfWork.TournamentRegisterRepository.GetFirstOrDefaultAsync(x => x.TournamentId == request.TournamentId);

                if (tournamentRegister != null)
                {
                    var dueTime = tournamentRegister.DueTime.Value.TimeOfDay;
                    var currentTime = DateTime.Now.TimeOfDay;

                    if (dueTime > currentTime)
                    {
                        return true;
                    }
                }

            return false;
        }



    }
}
