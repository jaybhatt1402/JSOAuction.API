using AutoMapper;
using JSOAuction.Data.Contexts;
using JSOAuction.Data.Infrastructure;
using JSOAuction.Domain.Entities.AuctionRegister;
using JSOAuction.Domain.Entities.PlayerRegister;
using JSOAuction.Domain.Entities.TeamRegister;
using JSOAuction.Services.Entities.Auction;
using JSOAuction.Services.Entities.PlayerRegister;
using JSOAuction.Services.Interfaces;
using System;
using System.Data;

namespace JSOAuction.Services.Services
{
    public class AuctionRegisterService : IAuctionRegisterService
    {
        private readonly IUnitOfWork<ReadOnlyApplicationDbContext> _readOnlyUnitOfWork;
        private readonly IUnitOfWork<ReadWriteApplicationDbContext> _readWriteUnitOfWork;
        private readonly ReadWriteApplicationDbContext _readWriteUnitOfWorkSP;
        private readonly IUnitOfWork<MasterDbContext> _masterDBContext;
        private readonly IMapper _mapper;
        public AuctionRegisterService(IUnitOfWork<ReadOnlyApplicationDbContext> readOnlyUnitOfWork,
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

        public async Task<List<AuctionRegister>> GetAllAuctionDetails()
        {
            IEnumerable<AuctionRegister> auctions = new List<AuctionRegister>();
            _readWriteUnitOfWorkSP.LoadStoredProc("GetAllAuctionDetails")
                .ExecuteStoredProc((handler) =>
                {
                    auctions = handler.ReadToList<AuctionRegister>();
                });
            if (auctions == null || !auctions.Any())
            {
                throw new Exception("No auctions found");
            }
            return auctions.ToList();
        }

        public async Task<int> SaveAuction(SaveAuctionDto request)
        {
            var saveAuction = new AuctionRegister()
            {
                AuctionName = request.AuctionName,
                Location = request.Location,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                CreatedBy = new Guid("e39f47a6-1c9b-4bb7-8ab1-67d6b8bb541b"),
                CreatedOn = DateTime.UtcNow,
                Year = request.Year,
                IsDeleted = false,
                IsActive = true,
                TournamentId = request.TournamentId
            };

            await _readWriteUnitOfWork.AuctionRegisterRepository.AddAsync(saveAuction);
            await _readWriteUnitOfWork.CommitAsync();

            return saveAuction.AuctionId;
        }

        public async Task<int> SaveTournamentAuction(SaveTournamentAuctionDto request)
        {
            var tournamentData = await _readWriteUnitOfWork.TournamentRegisterRepository.GetFirstOrDefaultAsync(x => x.TournamentId == request.TournamentId);

            var existingTournamentAuction = await _readWriteUnitOfWork.AuctionRegisterRepository.GetFirstOrDefaultAsync(x => x.TournamentId == request.TournamentId && x.IsDeleted == false);

            if (existingTournamentAuction != null)
            {
                return 1;
            }

            var saveTournamentAuction = new AuctionRegister()
            {
                TournamentId = tournamentData.TournamentId,
                AuctionName = tournamentData.TournamentName,
                Location = tournamentData.City,
                StartDate = tournamentData.StartDate,
                EndDate = tournamentData.EndDate,
                IsDeleted = false,
                IsActive = true,
                CreatedOn = DateTime.UtcNow,
                Year = tournamentData.StartDate.Value.Year,
                CreatedBy = new Guid("e39f47a6-1c9b-4bb7-8ab1-67d6b8bb541b"),
            };

            await _readWriteUnitOfWork.AuctionRegisterRepository.AddAsync(saveTournamentAuction);
            await _readWriteUnitOfWork.CommitAsync();

            return saveTournamentAuction.AuctionId;
        }

        public async Task<int> UpdateAuction(UpdateAuctionDto request)
        {
            var data = await _readWriteUnitOfWork.AuctionRegisterRepository.GetFirstOrDefaultAsync(x => x.AuctionId == request.AuctionId);
            if (data != null)
            {
                data.AuctionName = request.AuctionName;
                data.Location = request.Location;
                data.StartDate = request.StartDate;
                data.EndDate = request.EndDate;
                data.Year = request.Year;
                data.UpdatedOn = DateTime.UtcNow;
                data.UpdatedBy = new Guid("e39f47a6-1c9b-4bb7-8ab1-67d6b8bb541b");
                data.TournamentId = request.TournamentId;
                await _readWriteUnitOfWork.CommitAsync();
            }

            return data.AuctionId;
        }

        public async Task<bool> DeleteAuction(DeleteAuctionDto request)
        {
            int isuccess = 1;
            _readWriteUnitOfWorkSP.LoadStoredProc("DeleteAuction")
                .WithSqlParam("@AuctionId", request.AuctionId)
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

        public async Task<List<AuctionRegister>> GetAuctionById(int? AuctionId)
        {
            IEnumerable<AuctionRegister> auctions = new List<AuctionRegister>();
            _readWriteUnitOfWorkSP.LoadStoredProc("GetAuctionById")
                .WithSqlParam("@Id", AuctionId)
                .ExecuteStoredProc((handler) =>
                {
                    auctions = handler.ReadToList<AuctionRegister>();
                });
            if (auctions == null || !auctions.Any())
            {
                throw new Exception("No Auction found");
            }
            return auctions.ToList();
        }

        public async Task<List<AuctionRegister>> GetAuctionDetailsByTournament(int? TournamentId)
        {
            IEnumerable<AuctionRegister> auctions = new List<AuctionRegister>();
            _readWriteUnitOfWorkSP.LoadStoredProc("GetAuctionDetailsByTournament")
                .WithSqlParam("@TournamentId", TournamentId)
                .ExecuteStoredProc((handler) =>
                {
                    auctions = handler.ReadToList<AuctionRegister>();
                });
            if (auctions == null || !auctions.Any())
            {
                throw new Exception("No Auction found");
            }
            return auctions.ToList();
        }
    }
}
