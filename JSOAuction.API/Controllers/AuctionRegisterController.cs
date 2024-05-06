using AutoMapper;
using JSOAuction.API.Request.Auction;
using JSOAuction.API.Request.PlayerRegister;
using JSOAuction.Domain.Entities.AuctionRegister;
using JSOAuction.Domain.Entities.PlayerRegister;
using JSOAuction.Domain.Entities.TeamRegister;
using JSOAuction.Services.Entities.Auction;
using JSOAuction.Services.Entities.PlayerRegister;
using JSOAuction.Services.Interfaces;
using JSOAuction.Services.Services;
using JSOAuction.Utility;
using Microsoft.AspNetCore.Mvc;

namespace JSOAuction.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuctionRegisterController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IAuctionRegisterService _auctionRegisterService;
        public AuctionRegisterController(IAuctionRegisterService auctionRegisterService, IMapper mapper)
        {
            _mapper = mapper;
            _auctionRegisterService = auctionRegisterService;
        }
        [HttpGet("GetAllAuctionDetails")]
        public async Task<ActionResult<List<AuctionRegister>>> GetAllAuctionDetails()
        {
            var result = await _auctionRegisterService.GetAllAuctionDetails();
            return Ok(result);
        }
        [HttpPost("SaveAuction")]
        public async Task<Dictionary<string, object>> SaveAuction([FromBody] SaveAuctionRequest saveAuctionRequest)
        {
            var saveAuctionDto = _mapper.Map<SaveAuctionRequest, SaveAuctionDto>(saveAuctionRequest);
            var result = await _auctionRegisterService.SaveAuction(saveAuctionDto);
            return new Dictionary<string, object>() { { Constants.ResponseDataField, result } };
        }
        [HttpPost("UpdateAuction")]
        public async Task<Dictionary<string, object>> UpdateAuction([FromBody] UpdateAuctionRequest updateAuctionRequest)
        {
            var updateAuctionDto = _mapper.Map<UpdateAuctionRequest, UpdateAuctionDto>(updateAuctionRequest);
            var result = await _auctionRegisterService.UpdateAuction(updateAuctionDto);
            return new Dictionary<string, object>() { { Constants.ResponseDataField, result } };
        }
        [HttpPost("DeleteAuction")]
        public async Task<Dictionary<string, object>> DeleteAuction([FromBody] DeleteAuctionRequest request)
        {
            var deletePlayerDto = _mapper.Map<DeleteAuctionRequest, DeleteAuctionDto>(request);
            var result = await _auctionRegisterService.DeleteAuction(deletePlayerDto);
            return new Dictionary<string, object>() { { Constants.ResponseDataField, result } };
        }
        [HttpGet("GetAuctionById/{AuctionId}")]
        public async Task<ActionResult<List<AuctionRegister>>> GetAuctionById(int AuctionId)
        {
            var result = await _auctionRegisterService.GetAuctionById(AuctionId);
            return Ok(result);
        }
        [HttpGet("GetAuctionDetailsByTournament/{TournamentId}")]
        public async Task<ActionResult<List<AuctionRegister>>> GetAuctionDetailsByTournament(int TournamentId)
        {
            var result = await _auctionRegisterService.GetAuctionDetailsByTournament(TournamentId);
            return Ok(result);
        }
    }
}
