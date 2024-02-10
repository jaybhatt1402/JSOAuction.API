using AutoMapper;
using JSOAuction.Data.Contexts;
using JSOAuction.Data.Infrastructure;
using JSOAuction.Domain.Entities.PlayerRegister;
using JSOAuction.Services.Entities.PlayerRegister;
using JSOAuction.Services.Interfaces;
using JSOAuction.Utility;

namespace JSOAuction.Services.Services
{
    public class PlayerRegisterService : IPlayerRegisterService
    {
        private readonly IUnitOfWork<ReadOnlyApplicationDbContext> _readOnlyUnitOfWork;
        private readonly IUnitOfWork<ReadWriteApplicationDbContext> _readWriteUnitOfWork;
        private readonly ReadWriteApplicationDbContext _readWriteUnitOfWorkSP;
        private readonly IUnitOfWork<MasterDbContext> _masterDBContext;
        private readonly IMapper _mapper;
        public PlayerRegisterService(IUnitOfWork<ReadOnlyApplicationDbContext> readOnlyUnitOfWork,
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
        public async Task<Guid> SavePlayerRegister(SavePlayerRegisterDto request)
        {
            //Save Data in UserRegister Table.
            var hashPassword = GenericMethods.GetHash(request.Password);
            var savePlayerRegister = new PlayerRegister()
            {
                PlayerRegisterId = Guid.NewGuid(),
                FirstName = request.FirstName,
                LastName = request.LastName,
                Gender = request.Gender,
                MobileNo = request.MobileNo,
                AlternativePhoneNo = request.AlternativePhoneNo,
                Email = request.Email,
                DOB = request.DOB,
                Batsmen = request.Batsmen,
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

            };
            await _readWriteUnitOfWork.PlayerRegisterRepository.AddAsync(savePlayerRegister);
            await _readWriteUnitOfWork.CommitAsync();
            return savePlayerRegister.PlayerRegisterId;
        }
        public async Task<List<PlayerRegister>> GetAllPlayerDetails()
        {
            IEnumerable<PlayerRegister> players = new List<PlayerRegister>();
            _readWriteUnitOfWorkSP.LoadStoredProc("GetAllPlayerDetails")
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
    }
}
