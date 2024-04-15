using AutoMapper;
using JSOAuction.Data.Contexts;
using JSOAuction.Data.Infrastructure;
using JSOAuction.Domain.Entities.Tournament;
using JSOAuction.Services.Entities.Tournament;
using JSOAuction.Services.Interfaces;

namespace JSOAuction.Services.Services
{
    public class TournamentRegisterService : ITournamentRegisterService
    {
        private readonly IUnitOfWork<ReadOnlyApplicationDbContext> _readOnlyUnitOfWork;
        private readonly IUnitOfWork<ReadWriteApplicationDbContext> _readWriteUnitOfWork;
        private readonly ReadWriteApplicationDbContext _readWriteUnitOfWorkSP;
        private readonly IUnitOfWork<MasterDbContext> _masterDBContext;
        private readonly IMapper _mapper;
        public TournamentRegisterService(IUnitOfWork<ReadOnlyApplicationDbContext> readOnlyUnitOfWork,
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

        public async Task<List<TournamentRegister>> GetAllTournamentDetails()
        {
            IEnumerable<TournamentRegister> tournament = new List<TournamentRegister>();
            _readWriteUnitOfWorkSP.LoadStoredProc("GetAllTournamentDetails")
                .ExecuteStoredProc((handler) =>
                {
                    tournament = handler.ReadToList<TournamentRegister>();
                });
            if (tournament == null || !tournament.Any())
            {
                throw new Exception("No tournament found");
            }
            return tournament.ToList();
        }


        public async Task<int> SaveTournament(TournamentRegisterDto request)
        {
            var saveTournament = new TournamentRegister()
            {
                TournamentName = request.TournamentName,
                Description = request.Description,
                OrganizerName = request.OrganizerName,
                OrganizerContact = request.OrganizerContact,
                OrganizerEmail = request.OrganizerEmail,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                DueDate = request.DueDate,
                DueTime = request.DueTime,
                GroundAddress = request.GroundAddress,
                City = request.City,
                State = request.State,
                Country = request.Country,
                ZipCode = request.ZipCode,
                UploadBanner = request.UploadBanner,
                UploadLogo = request.UploadLogo,
                Open = request.Open,
                Corporate = request.Corporate,
                Community = request.Community,
                School = request.School,
                BoxCricket = request.BoxCricket,
                Series = request.Series,
                Other = request.Other,
                BallType = request.BallType,
                Overs = request.Overs,
                Format = request.Format,
                MaxTeams = request.MaxTeams,
                Gender = request.Gender,
                MinPlayer = request.MinPlayer,
                MaxPlayer = request.MaxPlayer,
                PaymentTerms = request.PaymentTerms,
                Amount = request.Amount,
                CreatedOn = DateTime.UtcNow,
                IsActive = true,
                IsDeleted = false
            };
             await _readWriteUnitOfWork.TournamentRegisterRepository.AddAsync(saveTournament);
            await _readWriteUnitOfWork.CommitAsync();
            return saveTournament.TournamentId;
        }


    }
}
