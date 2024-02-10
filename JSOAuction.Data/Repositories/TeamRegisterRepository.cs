using JSOAuction.Data.Contexts;
using JSOAuction.Data.Repositories.Interfaces;
using JSOAuction.Domain.Entities.PlayerRegister;
using JSOAuction.Domain.Entities.TeamRegister;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSOAuction.Data.Repositories
{
    public class TeamRegisterRepository<TContext> : BaseRepository<TeamRegister, TContext>, ITeamRegisterRepository<TContext> where TContext : IBaseContext
    {
        public TeamRegisterRepository(TContext unit) : base(unit)
        {

        }

    }
}
