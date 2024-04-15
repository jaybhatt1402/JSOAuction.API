using JSOAuction.Data.Contexts;
using JSOAuction.Domain.Entities.Tournament;

namespace JSOAuction.Data.Repositories.Interfaces
{
    public interface ITournamentRegisterRepository<TContext> : IBaseRepository<TournamentRegister, TContext> where TContext : IBaseContext
    {


    }
}
