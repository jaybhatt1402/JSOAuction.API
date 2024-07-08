using AutoMapper;
using JSOAuction.API.Request.ForgotPassword;
using JSOAuction.API.Request.SignUps;
using JSOAuction.Services.Entities.ForgotPassword;
using JSOAuction.Services.Entities.SignUps;

namespace JSOAuction.API.Infrastructure.Automapper
{
    public class ForgotPasswordMappingProfile : Profile
    {
        public ForgotPasswordMappingProfile()
        {
            CreateMap<ForgotPasswordRequest, ForgotPasswordDto>();

        }
    }
}
