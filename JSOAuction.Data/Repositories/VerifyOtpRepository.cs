using JSOAuction.Data.Contexts;
using JSOAuction.Data.Repositories.Interfaces;
using JSOAuction.Domain.Entities;

namespace JSOAuction.Data.Repositories
{
    public class VerifyOtpRepository<TContext> : BaseRepository<VerifyOtp, TContext>, IVerifyOtpRepository<TContext> where TContext : IBaseContext
    {
        public VerifyOtpRepository(TContext context) : base(context)
        {
        }
    }
}
