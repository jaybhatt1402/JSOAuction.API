using JSOAuction.Services.Entities.Common;
using System.ComponentModel;

namespace JSOAuction.API.Request.PlayerRegister
{
    public class AuctionPlayerRequest
    {
        public int? AuctionId { get; set; }
        public string? ScreenType { get; set; }
        public string? PlayerCategory { get; set; }
        public int? PlayerNo { get; set; }
        public int? TournamentId { get; set; }
    }

    public class TournamentPlayerRequest
    {
        public int TournamentId { get; set; }
        public PaginationDto Pagination { get; set; }
    }
    public class AuctionPaginationPlayerRequest
    {
        public int AuctionId { get; set; }
        public PaginationDto Pagination { get; set; }
    }
}
