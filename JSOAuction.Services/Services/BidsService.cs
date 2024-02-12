using AutoMapper;
using JSOAuction.Data.Contexts;
using JSOAuction.Data.Infrastructure;
using JSOAuction.Domain.Entities.PlayerRegister;
using JSOAuction.Domain.Entities.TeamRegister;
using JSOAuction.Services.Entities.Bids;
using JSOAuction.Services.Entities.PlayerRegister;
using JSOAuction.Services.Interfaces;
using JSOAuction.Utility;
using System.Data;

namespace JSOAuction.Services.Services
{
    public class BidsService : IBidsService
    {
        private readonly IUnitOfWork<ReadOnlyApplicationDbContext> _readOnlyUnitOfWork;
        private readonly IUnitOfWork<ReadWriteApplicationDbContext> _readWriteUnitOfWork;
        private readonly ReadWriteApplicationDbContext _readWriteUnitOfWorkSP;
        private readonly IUnitOfWork<MasterDbContext> _masterDBContext;
        private readonly IMapper _mapper;
        public BidsService(IUnitOfWork<ReadOnlyApplicationDbContext> readOnlyUnitOfWork,
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
        public async Task<bool> SaveBids(SaveBidsDto request)
        {
            string errorMsgValue = string.Empty;
            bool isuccess = true;

            //Save Data in UserRegister Table.
            _readWriteUnitOfWorkSP.LoadStoredProc("BidSave")
                .WithSqlParam("@TeamId", request.TeamId)
                .WithSqlParam("@PlayerId", request.PlayerId)
                .WithSqlParam("@AuctionId", request.AuctionId)
                .WithSqlParam("@BidAmount", request.BidAmount)
                //.WithSqlParam("@ModifiedBy", new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6"))
                .WithSqlParam("@ErrorMessage", errorMsgValue, DbType.String, ParameterDirection.Output)
                .ExecuteStoredProc((handler) =>
                {
                    //bidId = handler.GetValue("@BidIdOut").ToString();
                    errorMsgValue = handler.GetValue("@ErrorMessage").ToString();
                });

            if (errorMsgValue != string.Empty)
            {
                return isuccess = true;
            }
            else
            {
                return false;
            }

        }

        public async Task<bool> UndoBids(string bidId)
        {

            return false;
        }
    }
}
