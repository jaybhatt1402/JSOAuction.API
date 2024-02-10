using AutoMapper;
using JSOAuction.API.Request.PlayerRegister;
using JSOAuction.Domain.Entities.TeamRegister;
using JSOAuction.Services.Entities.PlayerRegister;
using JSOAuction.Services.Interfaces;
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
        [HttpPost("GetAllTeamDetails")]
        public async Task<ActionResult<List<TeamRegister>>> GetAllTeamDetails()
        {
            var result = await _teamRegisterService.GetAllTeamDetails();
            return Ok(result);
        }
    }
}
