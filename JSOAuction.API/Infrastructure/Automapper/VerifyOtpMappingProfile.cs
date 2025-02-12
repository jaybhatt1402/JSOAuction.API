using AutoMapper;
using JSOAuction.API.Request.Payments;
using JSOAuction.Services.Entities.Payments;
using JSOAuction.Domain.Entities.Payments;
using JSOAuction.API.Request.Groups;
using JSOAuction.Services.Entities.Groups;
using JSOAuction.Services.Entities.Payment;
using JSOAuction.API.Request;
using JSOAuction.Services.Entities;

namespace JSOAuction.API.Infrastructure.Automapper
{
    public class VerifyOtpMappingProfile : Profile
    {
        public VerifyOtpMappingProfile()
        {
            CreateMap<VerifyOtpRequest, VerifyOtpDto>();
        }
    }
}
