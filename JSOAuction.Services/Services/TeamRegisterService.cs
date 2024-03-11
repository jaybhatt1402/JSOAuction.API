using AutoMapper;
using JSOAuction.Data.Contexts;
using JSOAuction.Data.Infrastructure;
using JSOAuction.Domain.Entities.TeamRegister;
using JSOAuction.Services.Entities.PlayersDetailsByTeam;
using JSOAuction.Services.Interfaces;

namespace JSOAuction.Services.Services
{
    public class TeamRegisterService : ITeamRegisterService
    {
        private readonly IUnitOfWork<ReadOnlyApplicationDbContext> _readOnlyUnitOfWork;
        private readonly IUnitOfWork<ReadWriteApplicationDbContext> _readWriteUnitOfWork;
        private readonly ReadWriteApplicationDbContext _readWriteUnitOfWorkSP;
        private readonly IUnitOfWork<MasterDbContext> _masterDBContext;
        private readonly IMapper _mapper;
        public TeamRegisterService(IUnitOfWork<ReadOnlyApplicationDbContext> readOnlyUnitOfWork,
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

        public async Task<List<TeamRegister>> GetAllTeamDetails()
        {
            IEnumerable<TeamRegister> teams = new List<TeamRegister>();
            _readWriteUnitOfWorkSP.LoadStoredProc("GetAllRegisteredTeams")
                .ExecuteStoredProc((handler) =>
                {
                    teams = handler.ReadToList<TeamRegister>();
                });
            if (teams == null || !teams.Any())
            {
                throw new Exception("No teams found");
            }
            return teams.ToList();
        }

        public async Task<List<PlayersDetailsByTeamResponseModel>> GetPlayerDetailsByTeam(PlayerDetailsTeamWiseDto request)
        {
            IEnumerable<PlayersDetailsTeamWiseModel> playerDetails = new List<PlayersDetailsTeamWiseModel>();
            _readWriteUnitOfWorkSP.LoadStoredProc("GetPlayersDetailsByTeam")
                .WithSqlParam("@AuctionId", request.AuctionId)
                .ExecuteStoredProc((handler) =>
                {
                    playerDetails = handler.ReadToList<PlayersDetailsTeamWiseModel>();
                });

            var teamWisePlayerDetails = new List<PlayersDetailsByTeamResponseModel>();

            foreach (var teamGroup in playerDetails.GroupBy(pd => new { pd.TeamId, pd.TeamName, pd.TeamLogo }))
            {
                var teamModel = new PlayersDetailsByTeamResponseModel
                {
                    TeamId = teamGroup.Key.TeamId,
                    TeamName = teamGroup.Key.TeamName,
                    TeamLogo = teamGroup.Key.TeamLogo,
                    PlayerDetails = teamGroup.ToList()
                };

                teamWisePlayerDetails.Add(teamModel);
            }
            return teamWisePlayerDetails;

        }

        public async Task<List<TeamIdNameResponseModel>> GetTeamIdNameModel(TeamIdNameDto request)
        {
            IEnumerable<TeamIdNameResponseModel> teamDetails = new List<TeamIdNameResponseModel>();
            _readWriteUnitOfWorkSP.LoadStoredProc("GetTeamIdNameDetails")
                .WithSqlParam("@AuctionId", request.AuctionId)
                .ExecuteStoredProc((handler) =>
                {
                    teamDetails = handler.ReadToList<TeamIdNameResponseModel>();
                });

            if (teamDetails == null || !teamDetails.Any())
            {
                throw new Exception("No teams found");
            }
            return teamDetails.ToList();
        }
    }
}
