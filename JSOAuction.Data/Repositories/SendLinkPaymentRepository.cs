using JSOAuction.Domain.Entities.Payments;
using JSOAuction.Data.Contexts;
using JSOAuction.Data.Repositories.Interfaces;

namespace JSOAuction.Data.Repositories
{
    public class SendLinkPaymentRepository<TContext> : BaseRepository<SendLinkPayments, TContext>, ISendLinkPaymentRepository<TContext>
        where TContext : IBaseContext
    {
        public SendLinkPaymentRepository(TContext context) : base(context) { }
    }
}
