using AutoMapper;
using JSOAuction.API.Request.SignUps;
using JSOAuction.Services.Entities.SignUps;
using JSOAuction.Services.Interfaces;
using JSOAuction.Utility;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JSOAuction.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SignUpController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ISignUpService _signUpService;

        public SignUpController(ISignUpService signUpService, IMapper mapper)
        {
            _mapper = mapper;
            _signUpService = signUpService;
        }

        [HttpPost("SignUp")]
        public async Task<Dictionary<string, object>> SignUp([FromBody] SignUpRequest request)
        {
            var signUpDto = _mapper.Map<SignUpRequest, SignUpDto>(request);
            var result = await _signUpService.SignUpUser(signUpDto);
            return new Dictionary<string, object>() { { Constants.ResponseDataField, result } };
        }
    }
}
