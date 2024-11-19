using JSOAuction.Data.Contexts;
using JSOAuction.Data.Repositories.Interfaces;
using JSOAuction.Domain.Entities.Payments;

namespace JSOAuction.Data.Repositories
{
    public class PaymentRepository<TContext> : BaseRepository<Payments, TContext>, IPaymentRepository<TContext> where TContext : IBaseContext
    {
        public PaymentRepository(TContext unit) : base(unit)
        {
        }
    }
}
