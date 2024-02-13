using JSOAuction.Domain.Entities.PlayerRegister;
using JSOAuction.Domain.Entities.TeamRegister;
using JSOAuction.Services.Entities.Bids;
using JSOAuction.Services.Entities.PlayerRegister;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSOAuction.Services.Interfaces
{
    public interface IBidsService
    {
        Task<bool> SaveBids(SaveBidsDto request);
        Task<bool> UndoBids(string bidId);
        Task<List<AuctionTeamListResponseModel>> GetAuctionTeamList(AuctionTeamListDto auctionTeamListRequest);
    }
}
