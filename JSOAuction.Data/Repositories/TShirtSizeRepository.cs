// Repository Implementation
using JSOAuction.Data.Contexts;
using JSOAuction.Data.Repositories.Interfaces;
using JSOAuction.Domain.Entities;

namespace JSOAuction.Data.Repositories
{
    public class TShirtSizeRepository<TContext> : BaseRepository<TShirtSize, TContext>, ITShirtSizeRepository<TContext> where TContext : IBaseContext
    {
        public TShirtSizeRepository(TContext context) : base(context)
        {
        }
    }
}