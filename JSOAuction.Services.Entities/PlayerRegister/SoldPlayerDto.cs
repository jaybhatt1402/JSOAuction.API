using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSOAuction.Services.Entities.PlayerRegister
{
    public class SoldPlayerDto
    {
        public int? AuctionId { get; set; }
        public int? PlayerId { get; set; }
        public int? TeamId { get; set; }
        public int? BidId { get; set; }
        public string? Status { get; set; }  
    }
}
