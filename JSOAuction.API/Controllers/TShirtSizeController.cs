using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using JSOAuction.Services.Interfaces;
using System.Threading.Tasks;
using JSOAuction.Domain.Entities.AuctionRegister;
using JSOAuction.Services.Services;
using JSOAuction.Domain.Entities;

namespace JSOAuction.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TShirtSizeController : ControllerBase
    {
        private readonly ITShirtSizeService _tShirtSizeService;

        public TShirtSizeController(ITShirtSizeService tShirtSizeService)
        {
            _tShirtSizeService = tShirtSizeService;
        }

        [HttpGet("GetTShirtSizes")]
        public async Task<ActionResult<List<TShirtSize>>> GetTShirtSizes()
        {
            var result = await _tShirtSizeService.GetTShirtSizesAsync();
            return Ok(result);
        }

    }
}
