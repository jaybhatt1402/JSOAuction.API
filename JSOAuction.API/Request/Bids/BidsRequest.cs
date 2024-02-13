﻿using System.ComponentModel;

namespace JSOAuction.API.Request.Bids
{
    public class SaveBidsRequest
    {
        public int? PlayerId { get; set; }
        public int? TeamId { get; set; }
        public int? AuctionId { get; set; }
        public decimal? BidAmount { get; set; }
    }
}
