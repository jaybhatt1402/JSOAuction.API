using AutoMapper;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Upload;
using JSOAuction.Data.Contexts;
using JSOAuction.Data.Infrastructure;
using JSOAuction.Domain.Entities.PlayerRegister;
using JSOAuction.Services.Entities.Bids;
using JSOAuction.Services.Entities.PlayerRegister;
using JSOAuction.Services.Interfaces;
using JSOAuction.Utility;
using Microsoft.AspNetCore.Http;
using System.Data;
using System.Diagnostics;

namespace JSOAuction.Services.Services
{
    public class PlayerRegisterService : IPlayerRegisterService
    {
        private readonly IUnitOfWork<ReadOnlyApplicationDbContext> _readOnlyUnitOfWork;
        private readonly IUnitOfWork<ReadWriteApplicationDbContext> _readWriteUnitOfWork;
        private readonly ReadWriteApplicationDbContext _readWriteUnitOfWorkSP;
        private readonly IUnitOfWork<MasterDbContext> _masterDBContext;
        private readonly IMapper _mapper;
        public PlayerRegisterService(IUnitOfWork<ReadOnlyApplicationDbContext> readOnlyUnitOfWork,
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
        public async Task<int> SavePlayerRegister(SavePlayerRegisterDto request)
        {
            //Save Data in UserRegister Table.
            var hashPassword = GenericMethods.GetHash(request.Password);
            var savePlayerRegister = new PlayerRegister()
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Gender = request.Gender,
                MobileNo = request.MobileNo,
                AlternativePhoneNo = request.AlternativePhoneNo,
                Email = request.Email,
                DOB = request.DOB,
                Batsman = request.Batsman,
                Bowler = request.Bowler,
                WicketKeeper = request.WicketKeeper,
                BattingAllRounder = request.BattingAllRounder,
                BowlingAllRounder = request.BowlingAllRounder,
                PreviousTeamId = request.PreviousTeamId,
                LastPlayedYear = request.LastPlayedYear,
                ProfilePicture = request.ProfilePicture,
                Password = hashPassword,
                CreatedOn = DateTime.UtcNow,
                IsDeleted = false,
                IsActive = true,

            };
            await _readWriteUnitOfWork.PlayerRegisterRepository.AddAsync(savePlayerRegister);
            await _readWriteUnitOfWork.CommitAsync();
            return savePlayerRegister.PlayerRegisterId;
        }
        public async Task<List<PlayerRegister>> GetAllPlayerDetails()
        {
            IEnumerable<PlayerRegister> players = new List<PlayerRegister>();
            _readWriteUnitOfWorkSP.LoadStoredProc("GetAllPlayerDetails")
                .ExecuteStoredProc((handler) =>
                {
                    players = handler.ReadToList<PlayerRegister>();
                });
            if (players == null || !players.Any())
            {
                throw new Exception("No Players found");
            }
            return players.ToList();
        }

        public async Task<List<AuctionPlayerDetailsResponseModel>> GetAuctionPlayerDetails(AuctionPlayerDto request)
        {
            string errorMsgValue = string.Empty;
            IEnumerable<AuctionPlayerDetailsResponseModel> auctionPlayer = new List<AuctionPlayerDetailsResponseModel>();
            try
            {
                _readWriteUnitOfWorkSP.LoadStoredProc("GetAuctionPlayerDetails")
                    .WithSqlParam("@ScreenType", request.ScreenType)
                    .WithSqlParam("@AuctionId", request.AuctionId)
                    .ExecuteStoredProc((handler) =>
                    {
                        auctionPlayer = handler.ReadToList<AuctionPlayerDetailsResponseModel>();
                    });
            }
            catch (Exception ex)
            {
                throw new Exception("Invalid ScreenType provided. ScreenType must be either ''Admin'' or ''User''.", ex);
            }
            return auctionPlayer.ToList();
        }

        public async Task<bool> UpdatePlayerStatus(UpdatePlayerStatusDto request)
        {
            int isuccess = 1;
            _readWriteUnitOfWorkSP.LoadStoredProc("UpdatePlayerStatus")
                    .WithSqlParam("@AuctionId", request.AuctionId)
                    .WithSqlParam("@PlayerId", request.PlayerId)
                    .WithSqlParam("@Success", 0, DbType.Int32, ParameterDirection.Output)
                    .ExecuteStoredProc((handler) =>
                    {
                        isuccess = Convert.ToInt32(handler.GetValue("@Success"));
                    });

            if (isuccess > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public async Task<bool> SoldPlayer(SoldPlayerDto request)
        {
            int isuccess = 1;
            _readWriteUnitOfWorkSP.LoadStoredProc("SoldAuctionPlayer")
                    .WithSqlParam("@AuctionId", request.AuctionId)
                    .WithSqlParam("@PlayerId", request.PlayerId)
                    .WithSqlParam("@TeamId", request.TeamId != null ? request.TeamId : DBNull.Value, DbType.Int32)
                    .WithSqlParam("@BidId", request.BidId != null ? request.BidId : DBNull.Value, DbType.Int32)
                    .WithSqlParam("@Status", request.Status)
                    .WithSqlParam("@Success", 0, DbType.Int32, ParameterDirection.Output)
                    .ExecuteStoredProc((handler) =>
                    {
                        isuccess = Convert.ToInt32(handler.GetValue("@Success"));
                    });

            if (isuccess > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<int> SavePlayer(SavePlayerRegisterDto request)
        {
            string uploadId = "";
            DriveUploadBasic(request.UploadFile, ref uploadId);
            string webViewLink = "https://drive.google.com/thumbnail?id=" + uploadId + "&sz=w1000";

            //Save Data in UserRegister Table.
            var hashPassword = GenericMethods.GetHash(request.Password);
            var savePlayerRegister = new PlayerRegister()
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Gender = request.Gender,
                MobileNo = request.MobileNo,
                AlternativePhoneNo = request.AlternativePhoneNo,
                Email = request.Email,
                DOB = request.DOB,
                Batsman = request.Batsman,
                Bowler = request.Bowler,
                WicketKeeper = request.WicketKeeper,
                BattingAllRounder = request.BattingAllRounder,
                BowlingAllRounder = request.BowlingAllRounder,
                PreviousTeamId = request.PreviousTeamId,
                LastPlayedYear = request.LastPlayedYear,
                ProfilePicture = webViewLink,
                Password = hashPassword,
                CreatedOn = DateTime.UtcNow,
                IsDeleted = false,
                IsActive = true,

            };
            await _readWriteUnitOfWork.PlayerRegisterRepository.AddAsync(savePlayerRegister);
            await _readWriteUnitOfWork.CommitAsync();
            return savePlayerRegister.PlayerRegisterId;
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
    }
}
