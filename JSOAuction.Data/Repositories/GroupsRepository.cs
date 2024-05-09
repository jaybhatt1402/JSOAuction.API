using JSOAuction.Data.Contexts;
using JSOAuction.Data.Repositories.Interfaces;
using JSOAuction.Domain.Entities.Groups;
using JSOAuction.Domain.Entities.Tournament;

namespace JSOAuction.Data.Repositories
{
    public class GroupsRepository<TContext> : BaseRepository<Groups, TContext>, IGroupsRepository<TContext> where TContext : IBaseContext
    {
        public GroupsRepository(TContext unit) : base(unit)
        {

        }

    }
}
