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
    public class TeamDetailsByTournamentResponseModel
    {
        public int? TeamId { get; set; }
        public string? TeamName { get; set; }
        public string? OwnerName { get; set; }
        public string? CoachName { get; set; }
        public string? MobileNumber { get; set; }
        public string? Email { get; set; }
        public int? FoundedYear { get; set; }
        public string? TeamLogo { get; set; }
        public int? TournamentId { get; set; }
    }
}
