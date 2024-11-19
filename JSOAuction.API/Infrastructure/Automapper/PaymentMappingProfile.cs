using AutoMapper;
using JSOAuction.API.Request.Payments;
using JSOAuction.Services.Entities.Payments;
using JSOAuction.Domain.Entities.Payments;
using JSOAuction.API.Request.Groups;
using JSOAuction.Services.Entities.Groups;
using JSOAuction.Services.Entities.Payment;

namespace JSOAuction.API.Infrastructure.Automapper
{
    public class PaymentMappingProfile : Profile
    {
        public PaymentMappingProfile()
        {
            CreateMap<PaymentsRequest, PaymentDto>();
            CreateMap<PaymentDto, Payments>();
            CreateMap<SendLinkRequest, SendLinkDto>();
            CreateMap<SendLinkDto, SendLinkPayments>();
        }
    }
}
