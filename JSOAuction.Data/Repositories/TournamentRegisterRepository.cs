using JSOAuction.Data.Contexts;
using JSOAuction.Data.Repositories.Interfaces;
using JSOAuction.Domain.Entities.Tournament;

namespace JSOAuction.Data.Repositories
{
    public class TournamentRegisterRepository<TContext> : BaseRepository<TournamentRegister, TContext>, ITournamentRegisterRepository<TContext> where TContext : IBaseContext
    {
        public TournamentRegisterRepository(TContext unit) : base(unit)
        {

        }

    }
}
