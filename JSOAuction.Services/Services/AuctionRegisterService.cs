using AutoMapper;
using JSOAuction.Data.Contexts;
using JSOAuction.Data.Infrastructure;
using JSOAuction.Domain.Entities.AuctionRegister;
using JSOAuction.Domain.Entities.TeamRegister;
using JSOAuction.Services.Interfaces;

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
    }
}
