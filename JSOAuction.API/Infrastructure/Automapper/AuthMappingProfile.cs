using AutoMapper;
using JSOAuction.API.Request.User;
using JSOAuction.Services.Entities.User;

namespace JSOAuction.API.Infrastructure.Automapper
{
    public class AuthMappingProfile : Profile
    {
        public AuthMappingProfile()
        {
            CreateMap<UserAuthRequest, UserAuthRequestDto>();
        }
    }
}
