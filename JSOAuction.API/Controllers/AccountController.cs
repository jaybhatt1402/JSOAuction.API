using AutoMapper;
using JSOAuction.API.Request.User;
using JSOAuction.Services.Entities.User;
using JSOAuction.Services.Interfaces;
using JSOAuction.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JSOAuction.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IAuthenticationService _authenticationService;
        public AccountController(IAuthenticationService authenticationService, IMapper mapper)
        {
            _authenticationService = authenticationService;
            _mapper = mapper;
        }
        [HttpPost("authenticate")]
        public async Task<Dictionary<string, object>> AuthenticateAsync([FromBody] UserAuthRequest request)
        {
            var userDto = _mapper.Map<UserAuthRequest, UserAuthRequestDto>(request);
            var result = await _authenticationService.AuthenticateAsync(userDto, GetIdAddress());

            return new Dictionary<string, object>() { { Constants.ResponseDataField, result } };
        }
        #region Helper
        private string GetIdAddress()
        {
            // get source ip address for the current request
            if (Request.Headers.ContainsKey("X-Forwarded-For"))
                return Request.Headers["X-Forwarded-For"];
            else
                return HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
        }
        #endregion
    }
}
