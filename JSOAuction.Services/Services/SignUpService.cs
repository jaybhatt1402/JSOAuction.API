using AutoMapper;
using JSOAuction.Data.Contexts;
using JSOAuction.Data.Infrastructure;
using JSOAuction.Data.Repositories.Interfaces;
using JSOAuction.Domain.Entities.SignUps;
using JSOAuction.Domain.Entities.TeamRegister;
using JSOAuction.Services.Entities.SignUps;
using JSOAuction.Services.Interfaces;
using JSOAuction.Utility;
using System.Threading.Tasks;

namespace JSOAuction.Services.Services
{
    public class SignUpService : ISignUpService
    {
        private readonly IUnitOfWork<ReadOnlyApplicationDbContext> _readOnlyUnitOfWork;
        private readonly IUnitOfWork<ReadWriteApplicationDbContext> _readWriteUnitOfWork;
        private readonly ReadWriteApplicationDbContext _readWriteUnitOfWorkSP;
        private readonly IUnitOfWork<MasterDbContext> _masterDBContext;
        private readonly IMapper _mapper;

        public SignUpService(IUnitOfWork<ReadOnlyApplicationDbContext> readOnlyUnitOfWork,
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

        public async Task<object> SignUpUser(SignUpDto request)
        {
            var existingPlayer = await _readWriteUnitOfWork.SignUpRepository.GetAllAsync();

            var data = existingPlayer.ToList();

            var conditionData = data.Where(x => x.Mobile == request.Mobile && x.IsDeleted == false).ToList();

            if (conditionData.Count > 0)
            {
                return "Mobile number already registered.";
            }

            var newPassword = GenericMethods.GetHash(request.NewPassword);
            var confirmPassword = GenericMethods.GetHash(request.ConfirmPassword);
            var signUpUser = new SignUp()
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                EmailId = request.EmailId,
                Mobile = request.Mobile,
                NewPassword = newPassword,
                ConfirmPassword = confirmPassword,
                IsDeleted = false
            };
            await _readWriteUnitOfWork.SignUpRepository.AddAsync(signUpUser);
            await _readWriteUnitOfWork.CommitAsync();
            return signUpUser.Id;
        }
    }
}
