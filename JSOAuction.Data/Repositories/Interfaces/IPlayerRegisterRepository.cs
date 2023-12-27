using JSOAuction.Data.Contexts;
using JSOAuction.Domain.Entities.PlayerRegister;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSOAuction.Data.Repositories.Interfaces
{
    public interface IPlayerRegisterRepository<TContext> : IBaseRepository<PlayerRegister, TContext> where TContext : IBaseContext
    {


    }
}
