using System.ComponentModel;

namespace JSOAuction.API.Request.PlayerRegister
{
    public class GetPlayersDetailsByTeamRequest
    {
        public int? AuctionId { get; set; }
    }

    public class GetTeamIdNameModel
    {
        public int? AuctionId { get; set; }
    }
}
