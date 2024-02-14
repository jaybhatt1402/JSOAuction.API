using System.ComponentModel;

namespace JSOAuction.API.Request.Bids
{
    public class UndoBidsRequest
    {
        public int? AuctionId { get; set; }
        public int? BidId { get; set; }
    }
}
