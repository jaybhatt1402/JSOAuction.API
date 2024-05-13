using AutoMapper;
using JSOAuction.API.Request.Bids;
using JSOAuction.API.Request.Groups;
using JSOAuction.API.Request.PlayerRegister;
using JSOAuction.API.Request.Tournament;
using JSOAuction.Services.Entities.Bids;
using JSOAuction.Services.Entities.Groups;
using JSOAuction.Services.Entities.PlayerRegister;
using JSOAuction.Services.Entities.PlayersDetailsByTeam;
using JSOAuction.Services.Entities.Tournament;
using JSOAuction.Services.Interfaces;
using JSOAuction.Services.Services;
using JSOAuction.Utility;
using Microsoft.AspNetCore.Mvc;

namespace JSOAuction.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IGroupsService _groupsService;
        public GroupsController(IGroupsService groupsService, IMapper mapper)
        {
            _mapper = mapper;
            _groupsService = groupsService;
        }

        [HttpPost("SaveGroups")]
        public async Task<Dictionary<string, object>> SaveGroups([FromBody] SaveGroupsRequest request)
        {
            var saveGroupsDto = _mapper.Map<SaveGroupsRequest, SaveGroupsDto>(request);
            var result = await _groupsService.SaveGroups(saveGroupsDto);
            return new Dictionary<string, object>() { { Constants.ResponseDataField, result } };
        }

        [HttpPost("GetAllGroupsDetails")]
        public async Task<Dictionary<string, object>> GetAllGroupsDetails()
        {
            var result = await _groupsService.GetAllGroupsDetails();
            return new Dictionary<string, object>() { { Constants.ResponseDataField, result } };
        }

        [HttpPost("DeleteGroup")]
        public async Task<Dictionary<string, object>> DeleteGroup([FromBody] DeleteGroupsRequest request)
        {
            var deleteGroupDto = _mapper.Map<DeleteGroupsRequest, DeleteGroupsDto>(request);
            var result = await _groupsService.DeleteGroup(deleteGroupDto);
            return new Dictionary<string, object>() { { Constants.ResponseDataField, result } };
        }

        [HttpPost("UpdateGroup")]
        public async Task<Dictionary<string, object>> UpdateGroup([FromBody] UpdateGroupsRequest request)
        {

            var saveTournamentDto = _mapper.Map<UpdateGroupsRequest, UpdateGroupsDto>(request);

            var result = await _groupsService.UpdateGroup(saveTournamentDto);
            return new Dictionary<string, object>() { { Constants.ResponseDataField, result } };
        }

        [HttpPost("GetGroupListByTournamentId")]
        public async Task<Dictionary<string, object>> GetGroupListByTournamentId([FromBody] GroupListRequest request)
        {
            var groupListDto = _mapper.Map<GroupListRequest, GroupListDto>(request);
            var result = await _groupsService.GetGroupListByTournamentId(groupListDto);
            return new Dictionary<string, object>() { { Constants.ResponseDataField, result } };
        }
    }
}
