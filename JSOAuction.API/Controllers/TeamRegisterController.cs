using AutoMapper;
using JSOAuction.API.Request.PlayerRegister;
using JSOAuction.API.Request.Team;
using JSOAuction.API.Request.Tournament;
using JSOAuction.Domain.Entities.TeamRegister;
using JSOAuction.Services.Entities.PlayerRegister;
using JSOAuction.Services.Entities.PlayersDetailsByTeam;
using JSOAuction.Services.Entities.Team;
using JSOAuction.Services.Entities.Tournament;
using JSOAuction.Services.Interfaces;
using JSOAuction.Services.Services;
using JSOAuction.Utility;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        [HttpPost("GetAllTeamDetails")]
        public async Task<Dictionary<string, object>> GetAllTeamDetails([FromBody] GetTeamIdNameModel request)
        {
            var teamIdNameDto = _mapper.Map<GetTeamIdNameModel, TeamIdNameDto>(request);
            var result = await _teamRegisterService.GetAllTeamDetails(teamIdNameDto);
            return new Dictionary<string, object>() { { Constants.ResponseDataField, result } };
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
        [HttpPost("SaveTeam")]
        public async Task<Dictionary<string, object>> SaveTeam([FromBody] TeamRegisterRequest request)
        {

            var saveTeamDto = _mapper.Map<TeamRegisterRequest, TeamRegisterDto>(request);
            IFormFile uploadLogoFile = null;

            if (Request.Form.Files.Count > 0)
            {
                for (int i = 0; i < Request.Form.Files.Count; i++)
                {
                    var file = Request.Form.Files[i];
                    uploadLogoFile = file;
                }
            }
            saveTeamDto.UploadLogoFile = uploadLogoFile;
            var result = await _teamRegisterService.SaveTeam(saveTeamDto);
            return new Dictionary<string, object>() { { Constants.ResponseDataField, result } };
        }

        [HttpPost("DeleteTeam")]
        public async Task<Dictionary<string, object>> DeleteTeam([FromBody] DeleteTeamRequest request)
        {
            var deleteTournamentDto = _mapper.Map<DeleteTeamRequest, DeleteTeamDto>(request);
            var result = await _teamRegisterService.DeleteTeam(deleteTournamentDto);
            return new Dictionary<string, object>() { { Constants.ResponseDataField, result } };
        }
    }
}
