using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSOAuction.Services.Entities.PlayersDetailsByTeam
{
    public class PlayersDetailsByTeamResponseModel
    {
        public int TeamId { get; set; }
        public string TeamName { get; set; }
        public string TeamLogo { get; set; }
        public List<PlayersDetailsTeamWiseModel> PlayerDetails { get; set; }
    }

    public class TeamIdNameResponseModel
    {
        public int TeamId { get; set; }
        public string TeamName { get; set; }
    }
}
