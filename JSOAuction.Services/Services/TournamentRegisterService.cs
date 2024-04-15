using AutoMapper;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Upload;
using JSOAuction.Data.Contexts;
using JSOAuction.Data.Infrastructure;
using JSOAuction.Domain.Entities.Tournament;
using JSOAuction.Services.Entities.Tournament;
using JSOAuction.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Diagnostics;

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
            if (tournament == null || !tournament.Any())
            {
                throw new Exception("No tournament found");
            }
            return tournament.ToList();
        }


        public async Task<int> SaveTournament(TournamentRegisterDto request)
        {
            string uploadBannerId = "";

            string uploadLogoId = "";

            DriveUploadBasic(request.UploadBannerFile, ref uploadBannerId);

            string webViewLinkBanner = "https://drive.google.com/thumbnail?id=" + uploadBannerId + "&sz=w1000";

            DriveUploadBasic(request.UploadLogoFile, ref uploadLogoId);

            string webViewLinkLogo = "https://drive.google.com/thumbnail?id=" + uploadLogoId + "&sz=w1000";

            var saveTournament = new TournamentRegister()
            {
                TournamentName = request.TournamentName,
                Description = request.Description,
                OrganizerName = request.OrganizerName,
                OrganizerContact = request.OrganizerContact,
                OrganizerEmail = request.OrganizerEmail,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                DueDate = request.DueDate,
                DueTime = request.DueTime,
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
                IsDeleted = false
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


    }
}
