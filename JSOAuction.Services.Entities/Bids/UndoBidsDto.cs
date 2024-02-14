using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSOAuction.Services.Entities.Bids
{
    public class UndoBidsDto
    {
        public int? AuctionId { get; set; }
        public int? BidId { get; set; }
    }
}
