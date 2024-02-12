using JSOAuction.Data.Contexts;
using JSOAuction.Domain.Entities.AuctionRegister;
using JSOAuction.Domain.Entities.Bids;
using JSOAuction.Domain.Entities.PlayerRegister;
using JSOAuction.Domain.Entities.TeamRegister;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSOAuction.Data.Repositories.Interfaces
{
    public interface IBidsRepository<TContext> : IBaseRepository<Bids, TContext> where TContext : IBaseContext
    {


    }
}
