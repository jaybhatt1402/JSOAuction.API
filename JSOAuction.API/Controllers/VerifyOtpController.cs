using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using JSOAuction.API.Request.Payments;
using JSOAuction.Services.Entities.Payments;
using JSOAuction.Services.Interfaces;
using System.Threading.Tasks;
using JSOAuction.Services.Entities;
using JSOAuction.API.Request;

namespace JSOAuction.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VerifyOtpController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IVerifyOtpService _verifyOtpService;

        public VerifyOtpController(IVerifyOtpService verifyOtpService, IMapper mapper)
        {
            _mapper = mapper;
            _verifyOtpService = verifyOtpService;
        }

        [HttpPost("VerifyOtp")]
        public async Task<IActionResult> VerifyOtp([FromBody] VerifyOtpRequest request)
        {
            if (request == null || string.IsNullOrEmpty(request.Mobile))
            {
                return BadRequest(new { Success = false, Message = "Mobile number is required." });
            }

            var verifyOtpDto = _mapper.Map<VerifyOtpDto>(request);
            var result = await _verifyOtpService.VerifyOtpAsync(verifyOtpDto);
            return Ok(result);
        }
    }
}
