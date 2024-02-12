using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSOAuction.Services.Entities.Bids
{
    public class SaveBidsDto
    {
        public int? PlayerId{ get; set; }
        public int? TeamId { get; set; }
        public int? AuctionId { get; set; }
        public decimal? BidAmount { get; set; }
    }
}
