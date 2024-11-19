using System;
using System.Threading.Tasks;
using JSOAuction.Data.Repositories.Interfaces;
using JSOAuction.Services.Entities.Payments;
using JSOAuction.Services.Interfaces;
using JSOAuction.Domain.Entities.Payments;
using AutoMapper;
using JSOAuction.Data.Contexts;
using JSOAuction.Data.Infrastructure;
using JSOAuction.Domain.Entities.Groups;
using MailKit.Search;

namespace JSOAuction.Services.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IUnitOfWork<ReadOnlyApplicationDbContext> _readOnlyUnitOfWork;
        private readonly IUnitOfWork<ReadWriteApplicationDbContext> _readWriteUnitOfWork;
        private readonly IMapper _mapper;

        public PaymentService(IUnitOfWork<ReadOnlyApplicationDbContext> readOnlyUnitOfWork,
           IUnitOfWork<ReadWriteApplicationDbContext> readWriteUnitOfWork,
           IMapper mapper)
        {
            _readOnlyUnitOfWork = readOnlyUnitOfWork;
            _readWriteUnitOfWork = readWriteUnitOfWork;
            _mapper = mapper;
        }

        public async Task<dynamic> SavePaymentDataAsync(PaymentDto request)
        {
            try
            {
              
                var payment = _mapper.Map<Payments>(request);
                var savepayment = new Payments()
                {
                    PaymentId = request.PaymentId,
                    OrderId = request.OrderId,
                    Name = request.Name,
                    CreatedOn = DateTime.UtcNow, 
                    Amount = request.Amount,
                    Status = request.Status,
                    Contact = request.Contact,
                    Email = request.Email,
                };

                await _readWriteUnitOfWork.PaymentRepository.AddAsync(savepayment);
                await _readWriteUnitOfWork.CommitAsync();
                return new
                {
                    PaymentId = payment.PaymentId,
                    Status = payment.Status
                };
            }
            catch (Exception ex)
            {
                // Return error message if exception occurs
                return new
                {
                    Message = "Error saving payment",
                    Error = ex.Message
                };
            }
        }
    }
}
