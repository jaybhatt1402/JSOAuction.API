using AutoMapper;
using JSOAuction.API.Request.ForgotPassword;
using JSOAuction.Services.Entities.ForgotPassword;
using JSOAuction.Services.Interfaces;
using JSOAuction.Utility;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JSOAuction.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ForgotPasswordController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IForgotPasswordService _forgotPasswordService;

        public ForgotPasswordController(IForgotPasswordService forgotPasswordService, IMapper mapper)
        {
            _mapper = mapper;
            _forgotPasswordService = forgotPasswordService;
        }

        [HttpPost("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordRequest request)
        {
            var result = await _forgotPasswordService.ForgotPassword(new ForgotPasswordDto { EmailId = request.EmailId });
            return Ok(result);
        }

    }

}
