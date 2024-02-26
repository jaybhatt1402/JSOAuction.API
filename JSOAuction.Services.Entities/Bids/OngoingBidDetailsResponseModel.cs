using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSOAuction.Services.Entities.Bids
{
    public class OngoingBidDetailsResponseModel
    {
        public int? BidId { get; set; }
        public decimal? BidAmount { get; set; }
        public string? TeamName { get; set; }

        public int? TeamId { get; set; }
    }
}
