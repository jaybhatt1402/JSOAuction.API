using JSOAuction.Data.Contexts;
using JSOAuction.Domain.Entities.Payments;
using System.Threading.Tasks;

namespace JSOAuction.Data.Repositories.Interfaces
{
    public interface IPaymentRepository<TContext> : IBaseRepository<Payments, TContext> where TContext : IBaseContext
    {
    }
}
