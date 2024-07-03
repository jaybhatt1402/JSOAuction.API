using JSOAuction.Data.Contexts;
using JSOAuction.Domain.Entities.SignUps;
using JSOAuction.Domain.Entities.TeamRegister;
using JSOAuction.Services.Entities.SignUps;
using System.Threading.Tasks;

namespace JSOAuction.Data.Repositories.Interfaces
{
    public interface ISignUpRepository<TContext> : IBaseRepository<SignUp, TContext> where TContext : IBaseContext
    {


    }
}
