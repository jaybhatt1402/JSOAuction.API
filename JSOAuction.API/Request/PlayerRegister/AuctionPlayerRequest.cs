using System.ComponentModel;

namespace JSOAuction.API.Request.PlayerRegister
{
    public class AuctionPlayerRequest
    {
        public int? AuctionId { get; set; }
        public string? ScreenType { get; set; }
        public string? PlayerCategory { get; set; }
        public int? PlayerNo { get; set; }
    }
}
