using AutoMapper;
using JSOAuction.API.Request;
using JSOAuction.Domain.Entities;
using JSOAuction.Services.Entities;

namespace JSOAuction.API.Infrastructure.Automapper
{
    public class TShirtSizeMappingProfile : Profile
    {
        public TShirtSizeMappingProfile()
        {
            CreateMap<TShirtSizeRequest, TshirtSizeDto>();
            CreateMap<TShirtSize, TshirtSizeDto>().ReverseMap();
        }
    }
}