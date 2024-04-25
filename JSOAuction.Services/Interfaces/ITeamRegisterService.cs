using JSOAuction.Domain.Entities.TeamRegister;
using JSOAuction.Services.Entities.PlayerRegister;
using JSOAuction.Services.Entities.PlayersDetailsByTeam;
using JSOAuction.Services.Entities.Team;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSOAuction.Services.Interfaces
{
    public interface ITeamRegisterService
    {
        Task<List<TeamRegister>> GetAllTeamDetails(TeamIdNameDto teamIdNameDto);
        Task<List<PlayersDetailsByTeamResponseModel>> GetPlayerDetailsByTeam(PlayerDetailsTeamWiseDto request);
        Task<List<TeamIdNameResponseModel>> GetTeamIdNameModel(TeamIdNameDto request);
        Task<int> SaveTeam(TeamRegisterDto request);
        Task<bool> DeleteTeam(DeleteTeamDto request);
        Task<int> UpdateTeam(UpdateTeamDto request);
        Task<List<TeamDetailsByTournamentResponseModel>> GetTeamDetailsByTournament(GetTeamDetailsByTournamentDto request);
    }
}
