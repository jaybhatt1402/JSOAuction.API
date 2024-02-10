using JSOAuction.Data.Contexts;
using JSOAuction.Data.Repositories.Interfaces;
using JSOAuction.Domain.Entities.AuctionRegister;
using JSOAuction.Domain.Entities.PlayerRegister;
using JSOAuction.Domain.Entities.TeamRegister;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSOAuction.Data.Repositories
{
    public class AuctionRegisterRepository<TContext> : BaseRepository<AuctionRegister, TContext>, IAuctionRegisterRepository<TContext> where TContext : IBaseContext
    {
        public AuctionRegisterRepository(TContext unit) : base(unit)
        {

        }

    }
}
