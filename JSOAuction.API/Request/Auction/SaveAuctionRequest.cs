using System.ComponentModel;

namespace JSOAuction.API.Request.Auction
{
    public class SaveAuctionRequest
    {
        public int? Year { get; set; }
        public string? Location { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? AuctionName { get; set; }
        public decimal? StartBid { get; set; }
        public decimal? NextBid { get; set;}
        public int? TournamentId { get; set; }
    }
    public class SaveTournamentAuctionRequest
    {
        public int? TournamentId { get; set; }
    }

    public class UpdateAuctionRequest
    {
        public int AuctionId { get; set; }
        public int? Year { get; set; }
        public string? Location { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? AuctionName { get; set; }
        public decimal? StartBid { get; set; }
        public decimal? NextBid { get; set; }
        public int? TournamentId { get; set; }
    }
    public class DeleteAuctionRequest
    {
        public int? AuctionId { get; set; }
    }
}
