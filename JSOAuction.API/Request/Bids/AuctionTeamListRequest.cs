using System.ComponentModel;

namespace JSOAuction.API.Request.Bids
{
    public class AuctionTeamListRequest
    {
        public int? PlayerId { get; set; }
        public int? AuctionId { get; set; }
    }
}
