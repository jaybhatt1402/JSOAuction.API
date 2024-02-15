using System.ComponentModel;

namespace JSOAuction.API.Request.PlayerRegister
{
    public class UpdatePlayerStatusRequest
    {
        public int? AuctionId { get; set; }
        public int? PlayerId { get; set; }
    }
}
