using AutoMapper;
using JSOAuction.API.Request.ForgotPassword;
using JSOAuction.API.Request.PlayerRegister;
using JSOAuction.Services.Entities.ForgotPassword;
using JSOAuction.Services.Entities.PlayerRegister;
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
            var saveForgotPassword = _mapper.Map<ForgotPasswordRequest, ForgotPasswordDto>(request);

            var result = await _forgotPasswordService.ForgotPassword(saveForgotPassword);
            return Ok(result);
        }
        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword(ResetPasswordRequest request)
        {
            var saveResetPassword = _mapper.Map<ResetPasswordRequest, ResetPasswordDto>(request);

            var result = await _forgotPasswordService.ResetPassword(saveResetPassword);
            return Ok(result);
        }

    }

}
