using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSOAuction.Services.Entities.PlayersDetailsByTeam
{
    public class PlayersDetailsTeamWiseModel
    {
        public int PlayerId { get; set; }
        public int TeamId { get; set; }
        public string FullName { get; set; }
        public string MobileNo { get; set; }
        public bool Batsman { get; set; }
        public bool BattingAllRounder { get; set; }
        public bool Bowler { get; set; }
        public bool BowlingAllRounder { get; set; }
        public decimal BidAmount { get; set; }
        public string? TeamName { get; set; }
        public string? TeamLogo { get; set; }
        public bool WicketKeeper { get; set; }
    }
}
