using JSOAuction.Domain.Entities.Groups;
using JSOAuction.Domain.Entities.TeamRegister;
using JSOAuction.Domain.Entities.Tournament;
using JSOAuction.Services.Entities.Bids;
using JSOAuction.Services.Entities.Groups;
using JSOAuction.Services.Entities.PlayerRegister;
using JSOAuction.Services.Entities.PlayersDetailsByTeam;
using JSOAuction.Services.Entities.Tournament;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSOAuction.Services.Interfaces
{
    public interface IGroupsService
    {
        Task<object> SaveGroups(SaveGroupsDto request);
        Task<List<Groups>> GetAllGroupsDetails();
        Task<bool> DeleteGroup(DeleteGroupsDto request);
        Task<object> UpdateGroup(UpdateGroupsDto request);
        Task<List<GroupDetailsResponseModel>> GetGroupListByTournamentId (GroupListDto request);
    }
}
