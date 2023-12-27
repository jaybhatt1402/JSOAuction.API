using JSOAuction.Data.Contexts;
using JSOAuction.Data.Repositories.Interfaces;
using JSOAuction.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSOAuction.Data.Repositories
{
    public class RefreshTokenRepository<TContext> : BaseRepository<RefreshToken, TContext>, IRefreshTokenRepository<TContext> where TContext : IBaseContext
    {
        public RefreshTokenRepository(TContext context) : base(context)
        {
        }
    }
}
