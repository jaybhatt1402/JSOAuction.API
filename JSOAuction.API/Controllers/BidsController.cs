using AutoMapper;
using JSOAuction.API.Request.PlayerRegister;
using JSOAuction.Domain.Entities.TeamRegister;
using JSOAuction.Services.Entities.Bids;
using JSOAuction.Services.Entities.PlayerRegister;
using JSOAuction.Services.Interfaces;
using JSOAuction.Utility;
using Microsoft.AspNetCore.Mvc;

namespace JSOAuction.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BidsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IBidsService _bidsService;
        public BidsController(IBidsService bidsService, IMapper mapper)
        {
            _mapper = mapper;
            _bidsService = bidsService;
        }

        [HttpPost("SaveBids")]
        public async Task<Dictionary<string, object>> SaveBids([FromBody] SaveBidsRequest request)
        {
            var saveBidsDto = _mapper.Map<SaveBidsRequest, SaveBidsDto>(request);
            var result = await _bidsService.SaveBids(saveBidsDto);
            return new Dictionary<string, object>() { { Constants.ResponseDataField, result } };
        }
    }
}
