using AutoMapper;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Upload;
using JSOAuction.Data.Contexts;
using JSOAuction.Data.Infrastructure;
using JSOAuction.Domain.Entities.PlayerRegister;
using JSOAuction.Services.Entities.Bids;
using JSOAuction.Services.Entities.Common;
using JSOAuction.Services.Entities.PlayerRegister;
using JSOAuction.Services.Infrastructure;
using JSOAuction.Services.Interfaces;
using JSOAuction.Utility;
using JSOAuction.Utility.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OfficeOpenXml;
using System.Data;
using System.Diagnostics;
using System.Numerics;
using System.Reflection.Metadata;

namespace JSOAuction.Services.Services
{
    public class PlayerRegisterService : IPlayerRegisterService
    {
        private readonly IUnitOfWork<ReadOnlyApplicationDbContext> _readOnlyUnitOfWork;
        private readonly IUnitOfWork<ReadWriteApplicationDbContext> _readWriteUnitOfWork;
        private readonly ReadWriteApplicationDbContext _readWriteUnitOfWorkSP;
        private readonly IUnitOfWork<MasterDbContext> _masterDBContext;
        private readonly IMapper _mapper;
        private readonly ILogger<PlayerRegisterService> _logger;
        public PlayerRegisterService(IUnitOfWork<ReadOnlyApplicationDbContext> readOnlyUnitOfWork,
           IUnitOfWork<MasterDbContext> masterDBContext, IMapper mapper,
           IUnitOfWork<ReadWriteApplicationDbContext> readWriteUnitOfWork,
           ReadWriteApplicationDbContext readWriteUnitOfWorkSP,
           ILogger<PlayerRegisterService> logger)
        {
            _readOnlyUnitOfWork = readOnlyUnitOfWork;
            _masterDBContext = masterDBContext;
            _readWriteUnitOfWork = readWriteUnitOfWork;
            _mapper = mapper;
            _readWriteUnitOfWorkSP = readWriteUnitOfWorkSP;
            _logger = logger;
        }
        public async Task<int> SavePlayerRegister(SavePlayerRegisterDto request)
        {

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
                PaymentStatus = "Pending"

            };
            await _readWriteUnitOfWork.PlayerRegisterRepository.AddAsync(savePlayerRegister);
            await _readWriteUnitOfWork.CommitAsync();
            return savePlayerRegister.PlayerRegisterId;
        }
        public async Task<List<PlayerRegister>> GetAllPlayerDetails(int? AuctionId, PaginationDto paginationDto)
        {
            IEnumerable<PlayerRegister> players = new List<PlayerRegister>();
            _readWriteUnitOfWorkSP.LoadStoredProc("GetAllPlayerDetails")
                .WithSqlParam("@AuctionId", AuctionId)
                .ExecuteStoredProc((handler) =>
                {
                    players = handler.ReadToList<PlayerRegister>();
                });
            if (players == null || !players.Any())
            {
                throw new Exception("No Players found");
            }
            if (paginationDto != null)
            {
                if (!string.IsNullOrEmpty(paginationDto.GlobalSearch))
                {
                    players = players.Where(p => p.FirstName.Contains(paginationDto.GlobalSearch, StringComparison.OrdinalIgnoreCase));
                }

                if (!string.IsNullOrEmpty(paginationDto.OrderBy))
                {
                    bool descending = paginationDto.OrderDirection?.ToLower() == "desc";
                    players = players.OrderByPropertyName(paginationDto.OrderBy, descending);
                }

                if (paginationDto.PageSize.HasValue && paginationDto.PageIndex.HasValue)
                {
                    int skip = (paginationDto.PageIndex.Value - 1) * paginationDto.PageSize.Value;
                    players = players.Skip(skip).Take(paginationDto.PageSize.Value);
                }
            }
            return players.ToList();
        }

        public async Task<List<PlayerRegisterResponse>> GetAllPlayerDetailsWithTournamentID(int? TournamentId, PaginationDto paginationDto)
        {
            var teamData = _readWriteUnitOfWork.TeamRegisterRepository.GetAll().Where(x => x.TournamentId == TournamentId && x.IsDeleted == false);
            var playerData = from player in _readWriteUnitOfWork.PlayerRegisterRepository.GetAll()
                             join mapping in _readWriteUnitOfWork.AuctionPlayerMappingRepository.GetAll() on player.PlayerRegisterId equals mapping.PlayerId
                             where mapping.TournamentId == TournamentId && player.IsDeleted == false
                             select new
                             {
                                 PlayerId = player.PlayerRegisterId,
                                 PlayerName = player.FirstName,
                                 TournamentId = mapping.TournamentId
                                 
                             };
            var teamResult = teamData.ToList();

            var result = playerData.ToList();

            IEnumerable<PlayerRegisterResponse> players = new List<PlayerRegisterResponse>();
            _readWriteUnitOfWorkSP.LoadStoredProc("GetAllPlayerDetailsWithTournamentID")
                .WithSqlParam("@TournamentId", TournamentId)
                .ExecuteStoredProc((handler) =>
                {
                    players = handler.ReadToList<PlayerRegisterResponse>();
                });
            // if (players == null || !players.Any())
            // {
            //    return "No player found";
            //}
            if (paginationDto != null)
            {
                if (!string.IsNullOrEmpty(paginationDto.GlobalSearch))
                {
                    players = players.Where(p => p.FirstName.Contains(paginationDto.GlobalSearch, StringComparison.OrdinalIgnoreCase));
                }

                if (!string.IsNullOrEmpty(paginationDto.OrderBy))
                {
                    bool descending = paginationDto.OrderDirection?.ToLower() == "desc";
                    players = players.OrderByPropertyName(paginationDto.OrderBy, descending);
                }

                if (paginationDto.PageSize.HasValue && paginationDto.PageIndex.HasValue)
                {
                    int skip = (paginationDto.PageIndex.Value - 1) * paginationDto.PageSize.Value;
                    players = players.Skip(skip).Take(paginationDto.PageSize.Value);
                }
            }
            return players.ToList();
        }

        public async Task<byte[]> GetPlayerDetailsFileWithTournamentID(int? TournamentId)
        {


            DataTable dataTable = new DataTable();
            IEnumerable<PlayerRegister> players = new List<PlayerRegister>();
            _readWriteUnitOfWorkSP.LoadStoredProc("GetAllPlayerDetailsWithTournamentID")
                .WithSqlParam("@TournamentId", TournamentId)
                .ExecuteStoredProc((handler) =>
                {
                    dataTable = handler.ReadToTable();
                });

            if (dataTable == null)
            {
                throw new Exception("No Players found");
            }

            ExcelPackage.LicenseContext = LicenseContext.Commercial;

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Players");
                worksheet.Cells.LoadFromDataTable(dataTable, true);

                byte[] excelBytes = package.GetAsByteArray();
                return excelBytes;
            }
        }

        public async Task<object> GetAuctionPlayerDetails(AuctionPlayerDto request)
        {
            IEnumerable<AuctionPlayerDetailsResponseModel> auctionPlayer = new List<AuctionPlayerDetailsResponseModel>();
            int errorMsgValue = 0;
            try
            {
                errorMsgValue = _readWriteUnitOfWorkSP.LoadStoredProc("GetAuctionPlayerDetails")
                    .WithSqlParam("@ScreenType", request.ScreenType)
                    .WithSqlParam("@AuctionId", request.AuctionId)
                    .WithSqlParam("@PlayerNo", request.PlayerNo)
                    .WithSqlParam("@PlayerCategory", request.PlayerCategory)
                    .WithSqlParam("@TournamentId", request.TournamentId)
                    .WithSqlParam("@ReturnValue", 0, DbType.Int32, ParameterDirection.ReturnValue)
                    .ExecuteStoredProc((handler) =>
                    {
                        auctionPlayer = handler.ReadToList<AuctionPlayerDetailsResponseModel>();
                    });
            }
            catch (Exception ex)
            {
                throw new Exception("Invalid ScreenType provided. ScreenType must be either ''Admin'' or ''User''.", ex);
            }

            if (errorMsgValue > 0)
            {
                return "All players have been sold in this auction.";
            }
            else
            {
                //below changes we need to discuss
                if (auctionPlayer.ToList() != null)
                {
                    return auctionPlayer.ToList();
                }
                else
                {
                    return "Players are empty";
                }
            }
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
            var tournamentData = await _readWriteUnitOfWork.TournamentRegisterRepository.GetFirstOrDefaultAsync(x => x.TournamentId == request.TournamentId);

            var teamData = await _readWriteUnitOfWork.TeamRegisterRepository.GetFirstOrDefaultAsync(x => x.TeamId == request.TeamId);

            int isuccess = 1;
            if(teamData != null)
            {
                if (tournamentData.MaxPlayer > teamData.TeamSize)
                {
                    _readWriteUnitOfWorkSP.LoadStoredProc("SoldAuctionPlayer")
                            .WithSqlParam("@AuctionId", request.AuctionId)
                            .WithSqlParam("@PlayerId", request.PlayerId)
                            .WithSqlParam("@TeamId", request.TeamId != null ? request.TeamId : DBNull.Value, DbType.Int32)
                            .WithSqlParam("@BidId", request.BidId != null ? request.BidId : DBNull.Value, DbType.Int32)
                            .WithSqlParam("@Status", request.Status)
                            .WithSqlParam("@Success", 0, DbType.Int32, ParameterDirection.Output)
                            .WithSqlParam("@TournamentId", request.TournamentId)
                            .ExecuteStoredProc((handler) =>
                            {
                                isuccess = Convert.ToInt32(handler.GetValue("@Success"));
                            });
                }
                else
                {
                    isuccess = 0;
                }
            }
            else
            {
                    _readWriteUnitOfWorkSP.LoadStoredProc("SoldAuctionPlayer")
                            .WithSqlParam("@AuctionId", request.AuctionId)
                            .WithSqlParam("@PlayerId", request.PlayerId)
                            .WithSqlParam("@TeamId", request.TeamId != null ? request.TeamId : DBNull.Value, DbType.Int32)
                            .WithSqlParam("@BidId", request.BidId != null ? request.BidId : DBNull.Value, DbType.Int32)
                            .WithSqlParam("@Status", request.Status)
                            .WithSqlParam("@Success", 0, DbType.Int32, ParameterDirection.Output)
                            .WithSqlParam("@TournamentId", request.TournamentId)
                            .ExecuteStoredProc((handler) =>
                            {
                                isuccess = Convert.ToInt32(handler.GetValue("@Success"));
                            });
               
            }

            if (isuccess > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //    public async Task<object> SavePlayer(SavePlayerRegisterDto request)
        //    {
        //        var existingPlayer = await _readWriteUnitOfWork.PlayerRegisterRepository.GetFirstOrDefaultAsync(
        //            x => x.MobileNo == request.MobileNo && x.IsDeleted == false);

        //        if (existingPlayer != null)
        //        {
        //            return new { Success = false, Message = "Mobile number already registered." };
        //        }

        //        var playerData = from player in _readWriteUnitOfWork.PlayerRegisterRepository.GetAll()
        //                         join mapping in _readWriteUnitOfWork.AuctionPlayerMappingRepository.GetAll()
        //                         on player.PlayerRegisterId equals mapping.PlayerId
        //                         where mapping.TournamentId == request.TournamentId && player.IsDeleted == false
        //                         select new
        //                         {
        //                             PlayerId = player.PlayerRegisterId,
        //                             PlayerName = player.FirstName,
        //                             TournamentId = mapping.TournamentId
        //                         };

        //        var result = playerData.ToList();

        //        var tournamentData = await _readWriteUnitOfWork.TournamentRegisterRepository.GetFirstOrDefaultAsync(
        //            x => x.TournamentId == 1);

        //        var maxPlayerNo = _readWriteUnitOfWork.PlayerRegisterRepository.GetAll()
        //            .Join(
        //                _readWriteUnitOfWork.AuctionPlayerMappingRepository.GetAll(),
        //                player => player.PlayerRegisterId,
        //                mapping => mapping.PlayerId,
        //                (player, mapping) => new { player, mapping }
        //            )
        //            .Where(joined => joined.mapping.TournamentId == request.TournamentId && joined.player.IsDeleted == false)
        //            .Max(joined => (int?)joined.player.PlayerNo) ?? 0;

        //        int newPlayerNo = maxPlayerNo + 1;

        //        string uploadId = "";
        //        DriveUploadBasic(request.UploadFile, ref uploadId);

        //        string identityId = "";
        //        DriveUploadBasic(request.IdentityProof, ref identityId);


        //        string webViewLink = "https://drive.google.com/thumbnail?id=" + uploadId + "&sz=w1000";
        //        string identityProofLink = "https://drive.google.com/thumbnail?id=" + identityId + "&sz=w1000";

        //        var hashPassword = GenericMethods.GetHash(request.Password);
        //        var savePlayerRegister = new PlayerRegister()
        //        {
        //            FirstName = request.FirstName,
        //            LastName = request.LastName,
        //            Gender = request.Gender,
        //            BattingStyle = request.BattingStyle,
        //            BowlingStyle = request.BowlingStyle,
        //            MobileNo = request.MobileNo,
        //            AlternativePhoneNo = request.AlternativePhoneNo,
        //            Email = request.Email,
        //            DOB = request.DOB,
        //            Batsman = request.Batsman,
        //            Bowler = request.Bowler,
        //            WicketKeeper = request.WicketKeeper,
        //            BattingAllRounder = request.BattingAllRounder,
        //            BowlingAllRounder = request.BowlingAllRounder,
        //            PreviousTeamId = request.PreviousTeamId,
        //            LastPlayedYear = request.LastPlayedYear,
        //            ProfilePicture = webViewLink,
        //            Password = hashPassword,
        //            CreatedOn = DateTime.UtcNow,
        //            IsDeleted = false,
        //            IsActive = true,
        //            City = request.City,
        //            PlayerNo = newPlayerNo,
        //            PaymentStatus = "Pending",
        //            IdentityProof = identityProofLink,
        //            OccupationDetails = request.OccupationDetails,
        //            OccupationTypes = request.OccupationTypes,
        //            TShirtSizeId = request.TShirtSizeId
        //};

        //        await _readWriteUnitOfWork.PlayerRegisterRepository.AddAsync(savePlayerRegister);
        //        await _readWriteUnitOfWork.CommitAsync();

        //        var auctionPlayerMapping = new AuctionPlayerMapping()
        //        {
        //            PlayerId = savePlayerRegister.PlayerRegisterId,
        //            AuctionId = request.AuctionId,
        //            PlayerStatus = "notdisclosed",
        //            CreatedBy = new Guid("e39f47a6-1c9b-4bb7-8ab1-67d6b8bb541b"),
        //            CreatedOn = DateTime.UtcNow,
        //            TournamentId = 1,
        //        };

        //        await _readWriteUnitOfWork.AuctionPlayerMappingRepository.AddAsync(auctionPlayerMapping);
        //        await _readWriteUnitOfWork.CommitAsync();

        //        // Construct the response object with full data
        //        var response = new
        //        {
        //            Success = true,
        //            Message = "Player registered successfully.",
        //            Player = new
        //            {
        //                savePlayerRegister.PlayerRegisterId,
        //                savePlayerRegister.FirstName,
        //                savePlayerRegister.LastName,
        //                savePlayerRegister.Gender,
        //                savePlayerRegister.BattingStyle,
        //                savePlayerRegister.BowlingStyle,
        //                savePlayerRegister.MobileNo,
        //                savePlayerRegister.Email,
        //                savePlayerRegister.DOB,
        //                savePlayerRegister.City,
        //                savePlayerRegister.PlayerNo,
        //                savePlayerRegister.ProfilePicture,
        //                savePlayerRegister.CreatedOn
        //            },
        //            Tournament = new
        //            {
        //                tournamentData.TournamentId,
        //                tournamentData.TournamentName,
        //                tournamentData.MaxPlayer
        //            }
        //        };
        //        catch (Exception ex)
        //        {
        //        _logger.LogError(ex, "Error in SavePlayer method.");
        //        return new
        //        {
        //            Success = false,
        //            Message = "An error occurred while processing the request.",
        //            Error = ex.Message, // Returns the exception message in JSON response
        //            StackTrace = ex.StackTrace // (Optional) Stack trace for debugging
        //        };
        //    }

        //    return response;
        //    }

        //    public async Task<object> SavePlayer(SavePlayerRegisterDto request)
        //    {
        //        try
        //        {
        //            var existingPlayer = await _readWriteUnitOfWork.PlayerRegisterRepository.GetFirstOrDefaultAsync(
        //                x => x.MobileNo == request.MobileNo && x.IsDeleted == false);

        //            if (existingPlayer != null)
        //            {
        //                return new { Success = false, Message = "Mobile number already registered." };
        //            }

        //            var playerData = from player in _readWriteUnitOfWork.PlayerRegisterRepository.GetAll()
        //                             join mapping in _readWriteUnitOfWork.AuctionPlayerMappingRepository.GetAll()
        //                             on player.PlayerRegisterId equals mapping.PlayerId
        //                             where mapping.TournamentId == request.TournamentId && player.IsDeleted == false
        //                             select new
        //                             {
        //                                 PlayerId = player.PlayerRegisterId,
        //                                 PlayerName = player.FirstName,
        //                                 TournamentId = mapping.TournamentId
        //                             };

        //            var result = playerData.ToList();

        //            var tournamentData = await _readWriteUnitOfWork.TournamentRegisterRepository.GetFirstOrDefaultAsync(
        //                x => x.TournamentId == 1);

        //            var maxPlayerNo = _readWriteUnitOfWork.PlayerRegisterRepository.GetAll()
        //                .Join(
        //                    _readWriteUnitOfWork.AuctionPlayerMappingRepository.GetAll(),
        //                    player => player.PlayerRegisterId,
        //                    mapping => mapping.PlayerId,
        //                    (player, mapping) => new { player, mapping }
        //                )
        //                .Where(joined => joined.mapping.TournamentId == request.TournamentId && joined.player.IsDeleted == false)
        //                .Max(joined => (int?)joined.player.PlayerNo) ?? 0;

        //            int newPlayerNo = maxPlayerNo + 1;

        //            string uploadId = "";
        //            DriveUploadBasic(request.UploadFile, ref uploadId);

        //            string identityId = "";
        //            DriveUploadBasic(request.IdentityProof, ref identityId);

        //            string webViewLink = $"https://drive.google.com/thumbnail?id={uploadId}&sz=w1000";
        //            string identityProofLink = $"https://drive.google.com/thumbnail?id={identityId}&sz=w1000";

        //            //var hashPassword = GenericMethods.GetHash(request.Password);
        //            var savePlayerRegister = new PlayerRegister()
        //            {
        //                FirstName = request.FirstName,
        //                LastName = request.LastName,
        //                Gender = request.Gender,
        //                BattingStyle = request.BattingStyle,
        //                BowlingStyle = request.BowlingStyle,
        //                MobileNo = request.MobileNo,
        //                AlternativePhoneNo = request.AlternativePhoneNo,
        //                Email = request.Email,
        //                DOB = request.DOB,
        //                Batsman = request.Batsman,
        //                Bowler = request.Bowler,
        //                WicketKeeper = request.WicketKeeper,
        //                BattingAllRounder = request.BattingAllRounder,
        //                BowlingAllRounder = request.BowlingAllRounder,
        //                PreviousTeamId = request.PreviousTeamId,
        //                LastPlayedYear = request.LastPlayedYear,
        //                ProfilePicture = webViewLink,
        //                Password = null,
        //                CreatedOn = DateTime.UtcNow,
        //                IsDeleted = false,
        //                IsActive = true,
        //                City = request.City,
        //                PlayerNo = newPlayerNo,
        //                PaymentStatus = "Pending",
        //                IdentityProof = identityProofLink,
        //                OccupationDetails = request.OccupationDetails,
        //                OccupationTypes = request.OccupationTypes,
        //                TShirtSizeId = request.TShirtSizeId
        //            };

        //            await _readWriteUnitOfWork.PlayerRegisterRepository.AddAsync(savePlayerRegister);
        //            await _readWriteUnitOfWork.CommitAsync();

        //            var auctionPlayerMapping = new AuctionPlayerMapping()
        //            {
        //                PlayerId = savePlayerRegister.PlayerRegisterId,
        //                AuctionId = request.AuctionId,
        //                PlayerStatus = "notdisclosed",
        //                CreatedBy = new Guid("e39f47a6-1c9b-4bb7-8ab1-67d6b8bb541b"),
        //                CreatedOn = DateTime.UtcNow,
        //                TournamentId = 1,
        //            };

        //            await _readWriteUnitOfWork.AuctionPlayerMappingRepository.AddAsync(auctionPlayerMapping);
        //            await _readWriteUnitOfWork.CommitAsync();

        //        // Construct the response object with full data
        //        var response = new
        //        {
        //            Success = true,
        //            Message = "Player registered successfully.",
        //            Player = new
        //            {
        //                savePlayerRegister.PlayerRegisterId,
        //                savePlayerRegister.FirstName,
        //                savePlayerRegister.LastName,
        //                savePlayerRegister.Gender,
        //                savePlayerRegister.BattingStyle,
        //                savePlayerRegister.BowlingStyle,
        //                savePlayerRegister.MobileNo,
        //                savePlayerRegister.Email,
        //                savePlayerRegister.DOB,
        //                savePlayerRegister.City,
        //                savePlayerRegister.PlayerNo,
        //                savePlayerRegister.ProfilePicture,
        //                savePlayerRegister.CreatedOn,
        //                savePlayerRegister.OccupationDetails,
        //	savePlayerRegister.OccupationTypes,
        //	savePlayerRegister.IdentityProof,
        //	savePlayerRegister.TShirtSizeId,

        //},
        //            Tournament = new
        //            {
        //                tournamentData.TournamentId,
        //                tournamentData.TournamentName,
        //                tournamentData.MaxPlayer
        //            }
        //        };



        //            return response;
        //        }
        //        catch (Exception ex)
        //        {
        //            _logger.LogError(ex, "Error in SavePlayer method.");
        //            return new
        //            {
        //                Success = false,
        //                Message = "An error occurred while processing the request.",
        //                Error = ex.Message, // Returns the exception message in JSON response
        //                StackTrace = ex.StackTrace // (Optional) Stack trace for debugging
        //            };
        //        }
        //    }

        public async Task<object> SavePlayer(SavePlayerRegisterDto request)
        {
                var existingPlayer = await _readWriteUnitOfWork.PlayerRegisterRepository.GetFirstOrDefaultAsync(
                    x => x.MobileNo == request.MobileNo && x.IsDeleted == false);

                if (existingPlayer != null)
                {
                    return new { Success = false, Message = "Mobile number already registered." };
                }

            //    var playerData = from player in _readWriteUnitOfWork.PlayerRegisterRepository.GetAll()
            //                     join mapping in _readWriteUnitOfWork.AuctionPlayerMappingRepository.GetAll()
            //                     on player.PlayerRegisterId equals mapping.PlayerId
            //                     where mapping.TournamentId == request.TournamentId && player.IsDeleted == false
            //                     select new
            //                     {
            //                         PlayerId = player.PlayerRegisterId,
            //                         PlayerName = player.FirstName,
            //                         TournamentId = mapping.TournamentId
            //                     };
                
            //    if (playerData.Count.)
            //{
            //        var result = playerData.ToList();

            //}

                var tournamentData = await _readWriteUnitOfWork.TournamentRegisterRepository.GetFirstOrDefaultAsync(
                    x => x.TournamentId == 1);

                //var maxPlayerNo = _readWriteUnitOfWork.PlayerRegisterRepository.GetAll()
                //    .Join(
                //        _readWriteUnitOfWork.AuctionPlayerMappingRepository.GetAll(),
                //        player => player.PlayerRegisterId,
                //        mapping => mapping.PlayerId,
                //        (player, mapping) => new { player, mapping }
                //    )
                //    .Where(joined => joined.mapping.TournamentId == 1 && joined.player.IsDeleted == false)
                //    .Max(joined => (int?)joined.player.PlayerNo) ?? 0;

                //int newPlayerNo = maxPlayerNo + 1;

                string uploadId = "";
                DriveUploadBasic(request.UploadFile, ref uploadId);

                string identityId = "";
                DriveUploadBasic(request.IdentityProof, ref identityId);

                string webViewLink = $"https://drive.google.com/thumbnail?id={uploadId}&sz=w1000";
                string identityProofLink = $"https://drive.google.com/thumbnail?id={identityId}&sz=w1000";

                //var hashPassword = GenericMethods.GetHash(request.Password);
                var savePlayerRegister = new PlayerRegister()
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Gender = request.Gender,
                    BattingStyle = request.BattingStyle,
                    BowlingStyle = request.BowlingStyle,
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
                    Password = null,
                    CreatedOn = DateTime.UtcNow,
                    IsDeleted = false,
                    IsActive = true,
                    City = request.City,
                    //PlayerNo = newPlayerNo,
                    PaymentStatus = "Pending",
                    IdentityProof = identityProofLink,
                    OccupationDetails = request.OccupationDetails,
                    OccupationTypes = request.OccupationTypes,
                    TShirtSizeId = request.TShirtSizeId,
                    TShirtNumber = request.TShirtNumber,
                    TShirtName = request.TShirtName,
                    DonationAmount = request.DonationAmount
                };

                await _readWriteUnitOfWork.PlayerRegisterRepository.AddAsync(savePlayerRegister);
                await _readWriteUnitOfWork.CommitAsync();

                var auctionPlayerMapping = new AuctionPlayerMapping()
                {
                    PlayerId = savePlayerRegister.PlayerRegisterId,
                    AuctionId = request.AuctionId,
                    PlayerStatus = "notdisclosed",
                    CreatedBy = new Guid("e39f47a6-1c9b-4bb7-8ab1-67d6b8bb541b"),
                    CreatedOn = DateTime.UtcNow,
                    TournamentId = 1,
                };

                await _readWriteUnitOfWork.AuctionPlayerMappingRepository.AddAsync(auctionPlayerMapping);
                await _readWriteUnitOfWork.CommitAsync();

                // Construct the response object with full data
                var response = new
                {
                    Success = true,
                    Message = "Player registered successfully.",
                    Player = new
                    {
                        savePlayerRegister.PlayerRegisterId,
                        savePlayerRegister.FirstName,
                        savePlayerRegister.LastName,
                        savePlayerRegister.Gender,
                        savePlayerRegister.BattingStyle,
                        savePlayerRegister.BowlingStyle,
                        savePlayerRegister.MobileNo,
                        savePlayerRegister.Email,
                        savePlayerRegister.DOB,
                        savePlayerRegister.City,
                        savePlayerRegister.PlayerNo,
                        savePlayerRegister.ProfilePicture,
                        savePlayerRegister.CreatedOn,
                        savePlayerRegister.OccupationDetails,
                        savePlayerRegister.OccupationTypes,
                        savePlayerRegister.IdentityProof,
                        savePlayerRegister.TShirtName,
                        savePlayerRegister.TShirtNumber,
                        savePlayerRegister.TShirtSizeId,
                        savePlayerRegister.DonationAmount

                    },
                    Tournament = tournamentData != null ? new
                    {
                        tournamentData.TournamentId,
                        tournamentData.TournamentName,
                        tournamentData.MaxPlayer
                    } : null
                };

                return response;
            }



        public async Task<object> UpdatePlayer(UpdatePlayerDto request)
        {
            string uploadId = "";
            string identityId = "";
            //TODO
            //if (request.UploadFile == null)
            //{
            //    throw new Exception("Please Upload File.");
            //}

            string webViewLink = string.Empty;
            string identityLink = string.Empty;

            if (request.UploadFile != null)
            {
                DriveUploadBasic(request.UploadFile, ref uploadId);
                webViewLink = "https://drive.google.com/thumbnail?id=" + uploadId + "&sz=w1000";
            }
            if (request.IdentityProof != null)
            {
                DriveUploadBasic(request.IdentityProof, ref identityId);
                identityLink = "https://drive.google.com/thumbnail?id=" + identityId + "&sz=w1000";
            }
            //TODO
            //if (string.IsNullOrEmpty(uploadId))
            //{
            //    throw new Exception("File upload failed.");
            //}

            //Save Data in UserRegister Table.
            var existingPlayer = await _readWriteUnitOfWork.PlayerRegisterRepository.GetFirstOrDefaultAsync(x => x.MobileNo == request.MobileNo && x.PlayerRegisterId != request.PlayerRegisterId && x.IsDeleted == false);

            if (existingPlayer != null)
            {
                return "Mobile number is already in use";
            }

            var hashPassword = GenericMethods.GetHash(request.Password);
            var data = await _readWriteUnitOfWork.PlayerRegisterRepository.GetFirstOrDefaultAsync(x => x.PlayerRegisterId == request.PlayerRegisterId);
            if (data != null)
            {
                data.FirstName = request.FirstName;
                data.LastName = request.LastName;
                data.Gender = request.Gender;
                data.BattingStyle = request.BattingStyle;
                data.BowlingStyle = request.BowlingStyle;
                data.MobileNo = request.MobileNo;
                data.AlternativePhoneNo = request.AlternativePhoneNo;
                data.Email = request.Email;
                data.DOB = request.DOB;
                data.Batsman = request.Batsman;
                data.Bowler = request.Bowler;
                data.WicketKeeper = request.WicketKeeper;
                data.BattingAllRounder = request.BattingAllRounder;
                data.BowlingAllRounder = request.BowlingAllRounder;
                data.PreviousTeamId = request.PreviousTeamId;
                data.LastPlayedYear = request.LastPlayedYear;
                if (!string.IsNullOrEmpty(webViewLink))
                {
                    data.ProfilePicture = webViewLink;
                }
                if (!string.IsNullOrEmpty(identityLink))
                {
                    data.IdentityProof = identityLink;
                }
                data.Password = hashPassword;
                data.CreatedOn = DateTime.UtcNow;
                data.IsDeleted = false;
                data.IsActive = true;
                data.City = request.City;
                data.OccupationDetails = request.OccupationDetails;
                data.OccupationTypes = request.OccupationTypes;
                data.TShirtName = request.TShirtName;
                data.TShirtNumber = request.TShirtNumber;
                data.DonationAmount = request.DonationAmount;
                data.TShirtSizeId = request.TShirtSizeId;
                data.UpdatedBy = new Guid("e39f47a6-1c9b-4bb7-8ab1-67d6b8bb541b");
                data.UpdatedOn = DateTime.UtcNow;
                await _readWriteUnitOfWork.CommitAsync();
            }

            var auctionPlayerMappingData = await _readWriteUnitOfWork.AuctionPlayerMappingRepository.GetFirstOrDefaultAsync(x => x.PlayerId == request.PlayerRegisterId);
            if (auctionPlayerMappingData != null)
            {
                auctionPlayerMappingData.AuctionId = request.AuctionId;
                auctionPlayerMappingData.UpdatedBy = new Guid("e39f47a6-1c9b-4bb7-8ab1-67d6b8bb541b");
                auctionPlayerMappingData.UpdatedOn = DateTime.UtcNow;
                auctionPlayerMappingData.TournamentId = request.TournamentId;
                await _readWriteUnitOfWork.CommitAsync();
            }

            return data.PlayerRegisterId;
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

        public async Task<bool> DeletePlayer(DeletePlayerDto request)
        {
            int isuccess = 1;
            _readWriteUnitOfWorkSP.LoadStoredProc("DeletePlayer")
                .WithSqlParam("@PlayerRegisterId", request.PlayerRegisterId)
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

        public async Task<List<PlayerRegister>> GetPlayerById(int? PlayerRegisterId)
        {
            IEnumerable<PlayerRegister> players = new List<PlayerRegister>();
            _readWriteUnitOfWorkSP.LoadStoredProc("GetPlayerById")
                .WithSqlParam("@Id", PlayerRegisterId)
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

        public async Task<bool> AssignGroupToPlayers(PlayerRequestDto request)
        {
            try
            {
                foreach (var playerDto in request.Players)
                {

                    var data = await _readWriteUnitOfWork.PlayerRegisterRepository.GetFirstOrDefaultAsync(x => x.PlayerRegisterId == playerDto.PlayerId);

                    if (data != null)
                    {
                        data.PlayerGroupId = request.GroupId;
                        data.BasePrice = request.Baseprice;
                        data.UpdatedBy = new Guid("e39f47a6-1c9b-4bb7-8ab1-67d6b8bb541b");
                        data.UpdatedOn = DateTime.UtcNow;

                        await _readWriteUnitOfWork.CommitAsync();
                    }
                    else
                    {
                        return false;
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
