﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSOAuction.Services.Entities.PlayerRegister
{
    public class UpdatePlayerStatusDto
    {
        public int? AuctionId { get; set; }
        public int? PlayerId { get; set; }
    }
}
