using AutoMapper;
using JSOAuction.API.Request.PlayerRegister;
using JSOAuction.Domain.Entities.PlayerRegister;
using JSOAuction.Domain.Entities.TeamRegister;
using JSOAuction.Services.Entities.PlayerRegister;
using JSOAuction.Services.Interfaces;
using JSOAuction.Services.Services;
using JSOAuction.Utility;
using Microsoft.AspNetCore.Mvc;

namespace JSOAuction.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerRegisterController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IPlayerRegisterService _playerRegisterService;
        public PlayerRegisterController(IPlayerRegisterService playerRegisterService, IMapper mapper)
        {
            _mapper = mapper;
            _playerRegisterService = playerRegisterService;
        }
        [HttpPost("SavePlayerRegister")]
        public async Task<Dictionary<string, object>> SavePlayerRegister([FromBody] SavePlayerRegisterRequest request)
        {
            var savePlayerRegisterDto = _mapper.Map<SavePlayerRegisterRequest, SavePlayerRegisterDto>(request);
            var result = await _playerRegisterService.SavePlayerRegister(savePlayerRegisterDto);
            return new Dictionary<string, object>() { { Constants.ResponseDataField, result } };
        }

        [HttpPost("GetAllPlayerDetails")]
        public async Task<ActionResult<List<PlayerRegister>>> GetAllPlayerDetails()
        {
            var result = await _playerRegisterService.GetAllPlayerDetails();
            return Ok(result);
        }
    }
}
