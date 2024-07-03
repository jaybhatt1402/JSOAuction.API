using JSOAuction.Data.Contexts;
using JSOAuction.Data.Repositories.Interfaces;
using JSOAuction.Domain.Entities.SignUps;
using JSOAuction.Domain.Entities.TeamRegister;
using JSOAuction.Services.Entities.SignUps;
using System.Threading.Tasks;

namespace JSOAuction.Data.Repositories
{
    public class SignUpRepository<TContext> : BaseRepository<SignUp, TContext>, ISignUpRepository<TContext> where TContext : IBaseContext
    {
        public SignUpRepository(TContext unit) : base(unit)
        {

        }

    }
}
