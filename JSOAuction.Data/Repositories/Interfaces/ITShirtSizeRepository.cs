using JSOAuction.Data.Contexts;
using JSOAuction.Domain.Entities;

namespace JSOAuction.Data.Repositories.Interfaces
{
    public interface ITShirtSizeRepository<TContext> : IBaseRepository<TShirtSize, TContext> where TContext : IBaseContext
    {
    }
}
