using AutoMapper;
using JSOAuction.API.Request.Bids;
using JSOAuction.API.Request.PlayerRegister;
using JSOAuction.API.Request.Team;
using JSOAuction.API.Request.Tournament;
using JSOAuction.Domain.Entities.PlayerRegister;
using JSOAuction.Domain.Entities.TeamRegister;
using JSOAuction.Services.Entities.Bids;
using JSOAuction.Services.Entities.PlayerRegister;
using JSOAuction.Services.Entities.Team;
using JSOAuction.Services.Entities.Tournament;
using JSOAuction.Services.Interfaces;
using JSOAuction.Services.Services;
using JSOAuction.Utility;
using Microsoft.AspNetCore.Identity;
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
        private const string XlsxContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
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
        public async Task<ActionResult<List<PlayerRegister>>> GetAllPlayerDetails([FromBody] AuctionPaginationPlayerRequest request)
        {
            var result = await _playerRegisterService.GetAllPlayerDetails(request.AuctionId, request.Pagination);
            return Ok(result);
        }

        [HttpPost("GetAllPlayerDetailsWithTournamentID")]
        public async Task<ActionResult<List<PlayerRegister>>> GetAllPlayerDetailsWithTournamentID([FromBody] TournamentPlayerRequest request)
        {
            var result = await _playerRegisterService.GetAllPlayerDetailsWithTournamentID(request.TournamentId, request.Pagination);
            return Ok(result);
        }

        [HttpGet("GetPlayerDetailsFileWithTournamentID/{TournamentId}")]
        public async Task<ActionResult<byte[]>> GetPlayerDetailsFileWithTournamentID(int TournamentId)
        {
            byte[] excelBytes = await _playerRegisterService.GetPlayerDetailsFileWithTournamentID(TournamentId);
            return File(excelBytes, XlsxContentType, "PlayerDetails.xlsx"); ;
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
            IFormFile identityFile = null;
            if (!string.IsNullOrEmpty(request.LastPlayedYear))
            {
                savePlayerRegisterDto.LastPlayedYear = new DateTime(Convert.ToInt32(request.LastPlayedYear), 1, 1);
            }
            if (!string.IsNullOrEmpty(request.FirstName))
            {
                request.FirstName = char.ToUpper(request.FirstName[0]) + request.FirstName.Substring(1).ToLower();
            }
            if (!string.IsNullOrEmpty(request.LastName))
            {
                request.LastName = char.ToUpper(request.LastName[0]) + request.LastName.Substring(1).ToLower();
            }
            if (!string.IsNullOrEmpty(request.City))
            {
                request.City = char.ToUpper(request.City[0]) + request.City.Substring(1).ToLower();
            }
            if (Request.Form.Files.Count > 0)
            {
                if (Request.Form.Files.Where(x => x.Name == "file").Count() > 0)
                {
                    uploadFile = Request.Form.Files.Where(x => x.Name == "file").FirstOrDefault();
                }
                else
                {
                    uploadFile = null;
                }
                if (Request.Form.Files.Where(x => x.Name == "identity").Count() > 0)
                {
                    identityFile = Request.Form.Files.Where(x => x.Name == "identity").FirstOrDefault();
                }
                else
                {
                    identityFile = null;
                }
            }
            savePlayerRegisterDto.UploadFile = uploadFile;
            savePlayerRegisterDto.IdentityProof = identityFile;
            var result = await _playerRegisterService.SavePlayer(savePlayerRegisterDto);
            return new Dictionary<string, object>() { { Constants.ResponseDataField, result } };
        }


        [HttpPost("DeletePlayer")]
        public async Task<Dictionary<string, object>> DeletePlayer([FromBody] DeletePlayerRequest request)
        {
            var deletePlayerDto = _mapper.Map<DeletePlayerRequest, DeletePlayerDto>(request);
            var result = await _playerRegisterService.DeletePlayer(deletePlayerDto);
            return new Dictionary<string, object>() { { Constants.ResponseDataField, result } };
        }

        [HttpGet("GetPlayerById/{PlayerRegisterId}")]
        public async Task<ActionResult<List<PlayerRegister>>> GetPlayerById(int PlayerRegisterId)
        {
            var result = await _playerRegisterService.GetPlayerById(PlayerRegisterId);
            return Ok(result);
        }

        [HttpPost("UpdatePlayer")]
        public async Task<Dictionary<string, object>> UpdatePlayer([FromBody] UpdatePlayerRequest request)
        {

            var updatePlayerDto = _mapper.Map<UpdatePlayerRequest, UpdatePlayerDto>(request);
            IFormFile uploadFile = null;
            IFormFile identityFile = null;
            if (!string.IsNullOrEmpty(request.LastPlayedYear))
            {
                updatePlayerDto.LastPlayedYear = new DateTime(Convert.ToInt32(request.LastPlayedYear), 1, 1);
            }
            if (!string.IsNullOrEmpty(request.FirstName))
            {
                request.FirstName = char.ToUpper(request.FirstName[0]) + request.FirstName.Substring(1).ToLower();
            }
            if (!string.IsNullOrEmpty(request.LastName))
            {
                request.LastName = char.ToUpper(request.LastName[0]) + request.LastName.Substring(1).ToLower();
            }
            if (!string.IsNullOrEmpty(request.City))
            {
                request.City = char.ToUpper(request.City[0]) + request.City.Substring(1).ToLower();
            }
            if (Request.Form.Files.Count > 0)
            {
                //var identityfile = Request.Form.Files.Where(x => x.Name == "identity").FirstOrDefault();
                if (Request.Form.Files.Where(x => x.Name == "file").Count() > 0)
                {
                    uploadFile = Request.Form.Files.Where(x => x.Name == "file").FirstOrDefault();
                }
                else
                {
                    uploadFile = null;
                }
                if (Request.Form.Files.Where(x => x.Name == "identity").Count() > 0)
                {
                    identityFile = Request.Form.Files.Where(x => x.Name == "identity").FirstOrDefault();
                }
                else
                {
                    identityFile = null;
                }
            }

            updatePlayerDto.UploadFile = uploadFile;
            updatePlayerDto.IdentityProof = identityFile;
            var result = await _playerRegisterService.UpdatePlayer(updatePlayerDto);
            return new Dictionary<string, object>() { { Constants.ResponseDataField, result } };
        }

        [HttpPost("AssignGroupToPlayers")]
        public async Task<Dictionary<string, object>> AssignGroupToPlayers(PlayerRequestDto request)
        {
            //var assignGroupDto = _mapper.Map<PlayerRequest, PlayerRequestDto>(request);
            var result = await _playerRegisterService.AssignGroupToPlayers(request);
            return new Dictionary<string, object>() { { Constants.ResponseDataField, result } };
        }
    }
}
