using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSOAuction.Services.Entities.PlayerRegister
{
    public class AuctionPlayerDetailsResponseModel
    {
        public int? PlayerRegisterId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Gender { get; set; }
        public string? MobileNo { get; set; }
        public string? AlternativePhoneNo { get; set; }
        public string? Email { get; set; }
        public string? City { get; set; }
        public DateTime? DOB { get; set; }
        public bool? Batsman { get; set; }
        public bool? Bowler { get; set; }
        public bool? WicketKeeper { get; set; }
        public bool? BattingAllRounder { get; set; }
        public bool? BowlingAllRounder { get; set; }
        public int? PreviousTeamId { get; set; }
        public DateTime? LastPlayedYear { get; set; }
        public string? ProfilePicture { get; set; }
        public decimal? BasePrice { get; set; }
        public string? TeamName { get; set; }
        public decimal? BidAmount { get; set; }
        public int? TeamSize { get; set; }
        public decimal? RemainingBalance { get; set; }
        public decimal? MaximumBid { get; set; }
        public string? PlayerStatus { get; set; }
        public int? UnsoldCount { get; set; }
        public string? TeamLogo { get; set; }
        public bool IsVideoAvailable { get; set; }
        public string? ShowSoldPopup { get; set; }
        public string? OldTeamName { get; set; }
    }
}
