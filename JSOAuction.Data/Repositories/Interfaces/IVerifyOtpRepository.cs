using JSOAuction.Data.Contexts;
using JSOAuction.Domain.Entities;

namespace JSOAuction.Data.Repositories.Interfaces
{
    public interface IVerifyOtpRepository<TContext> : IBaseRepository<VerifyOtp, TContext> where TContext : IBaseContext
    {
    }
}
