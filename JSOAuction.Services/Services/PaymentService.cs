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

        public async Task<object> SavePaymentDataAsync(PaymentDto request)
        {
            try
            {
                // Validate the status field
                //if (string.IsNullOrEmpty(request.Status) ||
                //    (!request.Status.Equals("success", StringComparison.OrdinalIgnoreCase) &&
                //     !request.Status.Equals("failed", StringComparison.OrdinalIgnoreCase)))
                //{
                //    return new
                //    {
                //        Message = "Invalid payment status provided",
                //        Status = "failed"
                //    };
                //}

                // Map the DTO to the Domain Entity
                var payment = _mapper.Map<Payments>(request);
                payment.CreatedOn = DateTime.UtcNow;

                // Save the payment entity to the database
                await _readWriteUnitOfWork.PaymentRepository.AddAsync(payment);
                await _readWriteUnitOfWork.CommitAsync();

                // Return success response
                return new
                {
                    PaymentId = payment.PaymentId,
                    Status = payment.Status,
                    Message = payment.Status == "success" ? "Payment saved successfully" : "Payment marked as failed"
                };
            }
            catch (Exception ex)
            {
                // Log the exception and return a failure response
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