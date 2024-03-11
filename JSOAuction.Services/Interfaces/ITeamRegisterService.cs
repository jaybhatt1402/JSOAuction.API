using JSOAuction.Domain.Entities.TeamRegister;
using JSOAuction.Services.Entities.PlayerRegister;
using JSOAuction.Services.Entities.PlayersDetailsByTeam;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSOAuction.Services.Interfaces
{
    public interface ITeamRegisterService
    {
        Task<List<TeamRegister>> GetAllTeamDetails();
        Task<List<PlayersDetailsByTeamResponseModel>> GetPlayerDetailsByTeam(PlayerDetailsTeamWiseDto request);
        Task<List<TeamIdNameResponseModel>> GetTeamIdNameModel(TeamIdNameDto request);
    }
}
