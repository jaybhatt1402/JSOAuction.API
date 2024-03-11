using AutoMapper;
using JSOAuction.API.Request.PlayerRegister;
using JSOAuction.Domain.Entities.TeamRegister;
using JSOAuction.Services.Entities.PlayerRegister;
using JSOAuction.Services.Entities.PlayersDetailsByTeam;
using JSOAuction.Services.Interfaces;
using JSOAuction.Services.Services;
using JSOAuction.Utility;
using Microsoft.AspNetCore.Mvc;

namespace JSOAuction.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamRegisterController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ITeamRegisterService _teamRegisterService;
        public TeamRegisterController(ITeamRegisterService teamRegisterService, IMapper mapper)
        {
            _mapper = mapper;
            _teamRegisterService = teamRegisterService;
        }
        [HttpGet("GetAllTeamDetails")]
        public async Task<ActionResult<List<TeamRegister>>> GetAllTeamDetails()
        {
            var result = await _teamRegisterService.GetAllTeamDetails();
            return Ok(result);
        }
        [HttpPost("GetPlayerDetailsByTeam")]
        public async Task<Dictionary<string, object>> GetPlayerDetailsByTeam([FromBody] GetPlayersDetailsByTeamRequest request)
        {
            var playerDetailsTeamWiseDto = _mapper.Map<GetPlayersDetailsByTeamRequest, PlayerDetailsTeamWiseDto>(request);
            var result = await _teamRegisterService.GetPlayerDetailsByTeam(playerDetailsTeamWiseDto);
            return new Dictionary<string, object>() { { Constants.ResponseDataField, result } };
        }

        [HttpPost("GetTeamIdNameModel")]
        public async Task<Dictionary<string, object>> GetTeamIdNameModel([FromBody] GetTeamIdNameModel request)
        {
            var teamIdNameDto = _mapper.Map<GetTeamIdNameModel, TeamIdNameDto>(request);
            var result = await _teamRegisterService.GetTeamIdNameModel(teamIdNameDto);
            return new Dictionary<string, object>() { { Constants.ResponseDataField, result } };
        }
    }
}
