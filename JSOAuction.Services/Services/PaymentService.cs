using System;
using System.Data;
using System.Threading.Tasks;
using AutoMapper;
using JSOAuction.Data.Contexts;
using JSOAuction.Data.Infrastructure;
using JSOAuction.Data.Repositories.Interfaces;
using JSOAuction.Domain.Entities.Payments;
using JSOAuction.Services.Entities.Payments;
using JSOAuction.Services.Interfaces;

namespace JSOAuction.Services.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IUnitOfWork<ReadOnlyApplicationDbContext> _readOnlyUnitOfWork;
        private readonly IUnitOfWork<ReadWriteApplicationDbContext> _readWriteUnitOfWork;
        private readonly ReadWriteApplicationDbContext _readWriteUnitOfWorkSP;
        private readonly IUnitOfWork<MasterDbContext> _masterDBContext;
        private readonly IMapper _mapper;

        public PaymentService(
            IUnitOfWork<ReadOnlyApplicationDbContext> readOnlyUnitOfWork,
            IUnitOfWork<MasterDbContext> masterDBContext,
            IMapper mapper,
            IUnitOfWork<ReadWriteApplicationDbContext> readWriteUnitOfWork,
            ReadWriteApplicationDbContext readWriteUnitOfWorkSP)
        {
            _readOnlyUnitOfWork = readOnlyUnitOfWork;
            _masterDBContext = masterDBContext;
            _readWriteUnitOfWork = readWriteUnitOfWork;
            _mapper = mapper;
            _readWriteUnitOfWorkSP = readWriteUnitOfWorkSP;
        }

        public async Task<object> SavePaymentDataAsync(PaymentDto request)
        {

            try
            {
                var payment = _mapper.Map<Payments>(request);
                payment.CreatedOn = DateTime.UtcNow;

                await _readWriteUnitOfWork.PaymentRepository.AddAsync(payment);
                await _readWriteUnitOfWork.CommitAsync();

                int isuccess = 1;

                _readWriteUnitOfWorkSP.LoadStoredProc("UpdatePlayerPaymentStatus")
                    .WithSqlParam("@PlayerRegisterId", request.PlayerRegisterId)
                    .WithSqlParam("@PaymentStatus", request.Status)
                      .WithSqlParam("@Success", 0, DbType.Int32, ParameterDirection.Output)
                   .ExecuteStoredProc((handler) =>
                   {
                       isuccess = Convert.ToInt32(handler.GetValue("@Success"));
                   });

                if (isuccess > 0)
                {
                    return payment.Status;
                }
                return null;

            }
            catch (Exception ex)
            {
                return new
                {
                    Message = "Error saving payment",
                    Error = ex.Message,
                    Status = "failed"
                };
            }
        }
    }
}
