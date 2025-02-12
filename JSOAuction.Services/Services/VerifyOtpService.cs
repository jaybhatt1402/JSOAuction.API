using System.Threading.Tasks;
using AutoMapper;
using JSOAuction.Data.Contexts;
using JSOAuction.Data.Infrastructure;
using JSOAuction.Data.Repositories.Interfaces;
using JSOAuction.Services.Entities;
using JSOAuction.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace JSOAuction.Services.Services
{
    public class VerifyOtpService : IVerifyOtpService
    {
        private readonly IUnitOfWork<ReadOnlyApplicationDbContext> _readOnlyUnitOfWork;
        private readonly IUnitOfWork<ReadWriteApplicationDbContext> _readWriteUnitOfWork;
        private readonly IUnitOfWork<MasterDbContext> _masterDBContext;
        private readonly IVerifyOtpRepository<ReadWriteApplicationDbContext> _verifyOtpRepository;
        private readonly IMapper _mapper;

        public VerifyOtpService(
            IUnitOfWork<ReadOnlyApplicationDbContext> readOnlyUnitOfWork,
            IUnitOfWork<MasterDbContext> masterDBContext,
            IMapper mapper,
            IUnitOfWork<ReadWriteApplicationDbContext> readWriteUnitOfWork,
            IVerifyOtpRepository<ReadWriteApplicationDbContext> verifyOtpRepository)
        {
            _readOnlyUnitOfWork = readOnlyUnitOfWork;
            _masterDBContext = masterDBContext;
            _readWriteUnitOfWork = readWriteUnitOfWork;
            _mapper = mapper;
            _verifyOtpRepository = verifyOtpRepository;
        }

        public async Task<object> VerifyOtpAsync(VerifyOtpDto request)
        {
            if (string.IsNullOrEmpty(request.Mobile) || string.IsNullOrEmpty(request.Otp))
            {
                return new { Success = false, Message = "Mobile number and OTP are required." };
            }

            var userOtp = await _verifyOtpRepository.Query()
                .FirstOrDefaultAsync(u => u.Mobile == request.Mobile);

            if (userOtp == null)
            {
                return new { Success = false, Message = "Mobile number does not exist." };
            }

            if (userOtp.Otp != request.Otp)
            {
                return new { Success = false, Message = "Invalid OTP." };
            }

            // Mark OTP as verified
            userOtp.IsVerified = true;
            await _readWriteUnitOfWork.CommitAsync(); // Commit the transaction

            return new { Success = true, Message = "OTP verified successfully." };
        }
    }
}
