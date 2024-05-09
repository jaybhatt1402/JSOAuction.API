using AutoMapper;
using JSOAuction.API.Request.Bids;
using JSOAuction.API.Request.Groups;
using JSOAuction.API.Request.PlayerRegister;
using JSOAuction.API.Request.Tournament;
using JSOAuction.Domain.Entities.Bids;
using JSOAuction.Domain.Entities.PlayerRegister;
using JSOAuction.Domain.Entities.Tournament;
using JSOAuction.Services.Entities.Bids;
using JSOAuction.Services.Entities.Groups;
using JSOAuction.Services.Entities.PlayerRegister;
using JSOAuction.Services.Entities.PlayersDetailsByTeam;
using JSOAuction.Services.Entities.Tournament;

namespace JSOAuction.API.Infrastructure.Automapper
{
    public class GroupMappingProfile : Profile
    {
        public GroupMappingProfile()
        {
             CreateMap<SaveGroupsRequest, SaveGroupsDto>();
              CreateMap<DeleteGroupsRequest, DeleteGroupsDto>().ReverseMap();

             CreateMap<UpdateGroupsRequest, UpdateGroupsDto>();
        }
    }
}
