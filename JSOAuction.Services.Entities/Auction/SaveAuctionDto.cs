using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSOAuction.Services.Entities.Auction
{
    public class SaveAuctionDto
    {
        public int? Year { get; set; }
        public string? Location { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? AuctionName { get; set; }
        public decimal? StartBid { get; set; }
        public decimal? NextBid { get; set; }
        public int? TournamentId { get; set; }
    }

    public class UpdateAuctionDto
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
    public class DeleteAuctionDto
    {
        public int? AuctionId { get; set; }
    }
}
