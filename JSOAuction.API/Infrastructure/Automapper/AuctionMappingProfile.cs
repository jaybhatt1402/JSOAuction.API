using AutoMapper;
using JSOAuction.API.Request.Auction;
using JSOAuction.API.Request.Bids;
using JSOAuction.API.Request.PlayerRegister;
using JSOAuction.Domain.Entities.Bids;
using JSOAuction.Domain.Entities.PlayerRegister;
using JSOAuction.Services.Entities.Auction;
using JSOAuction.Services.Entities.Bids;
using JSOAuction.Services.Entities.PlayerRegister;
using JSOAuction.Services.Entities.PlayersDetailsByTeam;

namespace JSOAuction.API.Infrastructure.Automapper
{
    public class AuctionMappingProfile : Profile
    {
        public AuctionMappingProfile()
        {
            CreateMap<SaveAuctionRequest, SaveAuctionDto>().ReverseMap();
            CreateMap<UpdateAuctionRequest, UpdateAuctionDto>().ReverseMap();
            CreateMap<DeleteAuctionRequest, DeleteAuctionDto>().ReverseMap();
        }
    }
}
