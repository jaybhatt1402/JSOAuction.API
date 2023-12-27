using JSOAuction.Data.Contexts;
using JSOAuction.Data.Repositories.Interfaces;
using JSOAuction.Domain.Entities.PlayerRegister;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSOAuction.Data.Repositories
{
    public class PlayerRegisterRepository<TContext> : BaseRepository<PlayerRegister, TContext>, IPlayerRegisterRepository<TContext> where TContext : IBaseContext
    {
        public PlayerRegisterRepository(TContext unit) : base(unit)
        {

        }

    }
}
