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
    public class AuctionPlayerMappingRepository<TContext> : BaseRepository<AuctionPlayerMapping, TContext>, IAuctionPlayerMappingRepository<TContext> where TContext : IBaseContext
    {
        public AuctionPlayerMappingRepository(TContext unit) : base(unit)
        {

        }

    }
}
