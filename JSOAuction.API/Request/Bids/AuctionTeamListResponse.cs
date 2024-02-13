using System.ComponentModel;

namespace JSOAuction.API.Request.Bids
{
    public class AuctionTeamListResponse
    {
        public int? TeamId { get; set; }
        public string? TeamName { get; set; }
        public decimal? RemainingAmount { get; set; }
        public int? AuctionId { get; set; }
    }
}
