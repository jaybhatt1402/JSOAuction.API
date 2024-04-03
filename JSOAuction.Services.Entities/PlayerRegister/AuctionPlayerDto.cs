using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSOAuction.Services.Entities.PlayerRegister
{
    public class AuctionPlayerDto
    {
        public int? AuctionId { get; set; }
        public string? ScreenType { get; set; }
        public string? PlayerCategory { get; set; }
        public int? PlayerNo { get; set; }
    }
}
