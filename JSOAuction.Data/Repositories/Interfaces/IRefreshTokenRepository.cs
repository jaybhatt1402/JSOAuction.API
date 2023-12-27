using JSOAuction.Data.Contexts;
using JSOAuction.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSOAuction.Data.Repositories.Interfaces
{
    public interface IRefreshTokenRepository<TContext> : IBaseRepository<RefreshToken, TContext> where TContext : IBaseContext
    {

    }
}
