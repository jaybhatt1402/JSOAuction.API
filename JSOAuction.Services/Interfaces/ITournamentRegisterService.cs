using JSOAuction.Domain.Entities.TeamRegister;
using JSOAuction.Domain.Entities.Tournament;
using JSOAuction.Services.Entities.PlayerRegister;
using JSOAuction.Services.Entities.PlayersDetailsByTeam;
using JSOAuction.Services.Entities.Tournament;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSOAuction.Services.Interfaces
{
    public interface ITournamentRegisterService
    {
        Task<int> SaveTournament(TournamentRegisterDto request);
        Task<List<TournamentRegister>> GetAllTournamentDetails();
    }
}
