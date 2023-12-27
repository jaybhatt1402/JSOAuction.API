using JSOAuction.Data.Contexts;
using JSOAuction.Data.Repositories.Interfaces;
using JSOAuction.Domain.Entities.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSOAuction.Data.Repositories
{
    public class AccountsRepository<TContext> : BaseRepository<Account, TContext>, IAccountsRepository<TContext> where TContext : IBaseContext
    {
        public AccountsRepository(TContext unit) : base(unit)
        {

        }

    }
}
