using JSOAuction.Data.Contexts;
using JSOAuction.Domain.Entities.PlayerRegister;
using JSOAuction.Domain.Entities.TeamRegister;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSOAuction.Data.Repositories.Interfaces
{
    public interface ITeamRegisterRepository<TContext> : IBaseRepository<TeamRegister, TContext> where TContext : IBaseContext
    {


    }
}
