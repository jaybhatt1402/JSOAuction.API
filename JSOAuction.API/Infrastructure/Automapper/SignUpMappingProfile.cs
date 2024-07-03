using AutoMapper;
using JSOAuction.API.Request.SignUps;
using JSOAuction.Domain.Entities.SignUps;
using JSOAuction.Services.Entities.SignUps;

namespace JSOAuction.API.Infrastructure.Automapper
{
    public class SignUpMappingProfile : Profile
    {
        public SignUpMappingProfile()
        {
            CreateMap<SignUpRequest, SignUpDto>(); 
          
        }
    }
}
