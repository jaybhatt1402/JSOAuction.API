using System.ComponentModel;

namespace JSOAuction.API.Request.PlayerRegister
{
    public class SoldPlayerRequest
    {
        public int? AuctionId { get; set; }
        public int? PlayerId { get; set; }
        public int? TeamId { get; set; }
        public int? BidId { get; set; }
    }
}
