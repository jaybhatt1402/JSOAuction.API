using AutoMapper;
using JSOAuction.API.Request.PlayerRegister;
using JSOAuction.Domain.Entities.AuctionRegister;
using JSOAuction.Domain.Entities.TeamRegister;
using JSOAuction.Services.Entities.PlayerRegister;
using JSOAuction.Services.Interfaces;
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
    }
}
