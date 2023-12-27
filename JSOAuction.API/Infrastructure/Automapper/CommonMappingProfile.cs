using AutoMapper;
using JSOAuction.API.Request.Common;
using JSOAuction.Services.Entities.Common;

namespace JSOAuction.API.Infrastructure.Automapper
{
    public class CommonMappingProfile : Profile
    {
        public CommonMappingProfile()
        {
            CreateMap<PaginationRequest, PaginationDto>();
        }
    }
}
