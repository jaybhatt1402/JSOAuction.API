using JSOAuction.Data.Contexts;
using JSOAuction.Domain.Entities.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSOAuction.Data.Repositories.Interfaces
{
    public interface IAccountsRepository<TContext> : IBaseRepository<Account, TContext> where TContext : IBaseContext
    {
    }
}
