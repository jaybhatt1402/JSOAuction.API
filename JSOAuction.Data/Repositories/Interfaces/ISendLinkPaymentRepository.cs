using JSOAuction.Domain.Entities.Payments;
using JSOAuction.Data.Contexts;

namespace JSOAuction.Data.Repositories.Interfaces
{
    public interface ISendLinkPaymentRepository<TContext> : IBaseRepository<SendLinkPayments, TContext>
        where TContext : IBaseContext
    {
    }
}
