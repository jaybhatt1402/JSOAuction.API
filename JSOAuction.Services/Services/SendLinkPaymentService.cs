using AutoMapper;
using JSOAuction.Data.Repositories.Interfaces;
using JSOAuction.Domain.Entities.Payments;
using JSOAuction.Services.Entities.Payment;
using JSOAuction.Services.Interfaces;
using System;
using System.Threading.Tasks;
using JSOAuction.Data.Infrastructure;
using JSOAuction.Data.Contexts;

namespace JSOAuction.Services.Services
{
    public class SendLinkPaymentService : ISendLinkPaymentService
    {
        private readonly IUnitOfWork<ReadOnlyApplicationDbContext> _readOnlyUnitOfWork;
        private readonly IUnitOfWork<ReadWriteApplicationDbContext> _readWriteUnitOfWork;
        private readonly IMapper _mapper;

        public SendLinkPaymentService(IUnitOfWork<ReadOnlyApplicationDbContext> readOnlyUnitOfWork,
           IUnitOfWork<ReadWriteApplicationDbContext> readWriteUnitOfWork,
           IMapper mapper)
        {
            _readOnlyUnitOfWork = readOnlyUnitOfWork;
            _readWriteUnitOfWork = readWriteUnitOfWork;
            _mapper = mapper;
        }

        public async Task<object> SendLinkPaymentDataAsync(SendLinkDto request)
        {
            try
            {
                var sendLinkPayment = _mapper.Map<SendLinkPayments>(request);

                await _readWriteUnitOfWork.SendLinkPaymentRepository.AddAsync(sendLinkPayment);
                await _readWriteUnitOfWork.CommitAsync();

                return new
                {
                    Message = "Payment link sent successfully",
                    Data = sendLinkPayment
                };
            }
            catch (Exception ex)
            {
                return new
                {
                    Message = "Error occurred while sending payment link",
                    Error = ex.Message
                };
            }
        }
    }
}
