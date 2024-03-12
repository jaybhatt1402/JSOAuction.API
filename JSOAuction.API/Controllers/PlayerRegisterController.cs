using AutoMapper;
using JSOAuction.API.Request.Bids;
using JSOAuction.API.Request.PlayerRegister;
using JSOAuction.Domain.Entities.PlayerRegister;
using JSOAuction.Domain.Entities.TeamRegister;
using JSOAuction.Services.Entities.Bids;
using JSOAuction.Services.Entities.PlayerRegister;
using JSOAuction.Services.Interfaces;
using JSOAuction.Services.Services;
using JSOAuction.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

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

        [HttpGet("GetAllPlayerDetails")]
        public async Task<ActionResult<List<PlayerRegister>>> GetAllPlayerDetails()
        {
            var result = await _playerRegisterService.GetAllPlayerDetails();
            return Ok(result);
        }

        [HttpPost("GetAuctionPlayer")]
        public async Task<Dictionary<string, object>> GetAuctionPlayer([FromBody] AuctionPlayerRequest request)
        {
            var auctionPlayerDto = _mapper.Map<AuctionPlayerRequest, AuctionPlayerDto>(request);
            var result = await _playerRegisterService.GetAuctionPlayerDetails(auctionPlayerDto);
            return new Dictionary<string, object>() { { Constants.ResponseDataField, result } };
        }

        [HttpPost("UpdatePlayerStatus")]
        public async Task<Dictionary<string, object>> UpdatePlayerStatus([FromBody] UpdatePlayerStatusRequest request)
        {
            var updatePlayerStatusDto = _mapper.Map<UpdatePlayerStatusRequest, UpdatePlayerStatusDto>(request);
            var result = await _playerRegisterService.UpdatePlayerStatus(updatePlayerStatusDto);
            return new Dictionary<string, object>() { { Constants.ResponseDataField, result } };
        }

        [HttpPost("SoldPlayer")]
        public async Task<Dictionary<string, object>> SoldPlayer([FromBody] SoldPlayerRequest request)
        {
            var soldPlayerDto = _mapper.Map<SoldPlayerRequest, SoldPlayerDto>(request);
            var result = await _playerRegisterService.SoldPlayer(soldPlayerDto);
            return new Dictionary<string, object>() { { Constants.ResponseDataField, result } };
        }

        [HttpPost("SavePlayer")]
        public async Task<Dictionary<string, object>> SavePlayer([FromBody] SavePlayerRegisterRequest request)
        {
            var savePlayerRegisterDto = _mapper.Map<SavePlayerRegisterRequest, SavePlayerRegisterDto>(request);
            IFormFile uploadFile = null;
            if (!string.IsNullOrEmpty(request.LastPlayedYear))
            {
                savePlayerRegisterDto.LastPlayedYear = new DateTime(Convert.ToInt32(request.LastPlayedYear), 1, 1);
            }
            if (Request.Form.Files.Count > 0)
            {
                uploadFile = Request.Form.Files[0];
            }
            savePlayerRegisterDto.UploadFile = uploadFile;
            var result = await _playerRegisterService.SavePlayer(savePlayerRegisterDto);
            return new Dictionary<string, object>() { { Constants.ResponseDataField, result } };
        }
    }
}
