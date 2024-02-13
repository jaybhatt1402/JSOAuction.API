using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSOAuction.Services.Entities.Bids
{
    public class AuctionTeamListResponseModel
    {
        public int? TeamId { get; set; }
        public string? TeamName { get; set; }
        public decimal? RemainingAmount { get; set; }
    }
}
