using System.ComponentModel;

namespace JSOAuction.API.Request.PlayerRegister
{
    public class AuctionPlayerRequest
    {
        public int? AuctionId { get; set; }
        public string? ScreenType { get; set; }
    }
}
