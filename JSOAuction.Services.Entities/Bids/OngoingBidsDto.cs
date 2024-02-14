using System.ComponentModel;

namespace JSOAuction.Services.Entities.Bids
{
    public class OngoingBidsDto
    {
        public int? PlayerId { get; set; }
        public int? AuctionId { get; set; }
    }
}
