using AutoMapper;
using JSOAuction.API.Request.Bids;
using JSOAuction.Services.Entities.Bids;
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

        [HttpPost("GetAuctionTeamsList")]
        public async Task<Dictionary<string, object>> GetAuctionTeamsList([FromBody] AuctionTeamListRequest request)
        {
            var auctionTeamListDto = _mapper.Map<AuctionTeamListRequest, AuctionTeamListDto>(request);
            var result = await _bidsService.GetAuctionTeamList(auctionTeamListDto);
            return new Dictionary<string, object>() { { Constants.ResponseDataField, result } };
        }

        [HttpPost("GetOngoingBidDetails")]
        public async Task<Dictionary<string, object>> GetOngoingBidDetails([FromBody] OngoingBidsRequest request)
        {
            var ongoingBidsDto = _mapper.Map<OngoingBidsRequest, OngoingBidsDto>(request);
            var result = await _bidsService.GetOngoingBidDetails(ongoingBidsDto);
            return new Dictionary<string, object>() { { Constants.ResponseDataField, result } };
        }

        [HttpPost("UndoBids")]
        public async Task<Dictionary<string, object>> UndoBids([FromBody] UndoBidsRequest request)
        {
            var undoBidsDto = _mapper.Map<UndoBidsRequest, UndoBidsDto>(request);
            var result = await _bidsService.UndoAuctionBid(undoBidsDto);
            return new Dictionary<string, object>() { { Constants.ResponseDataField, result } };
        }
    }
}
