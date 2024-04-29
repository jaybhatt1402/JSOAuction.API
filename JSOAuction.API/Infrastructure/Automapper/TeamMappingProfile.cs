using AutoMapper;
using JSOAuction.API.Request.Bids;
using JSOAuction.API.Request.PlayerRegister;
using JSOAuction.API.Request.Team;
using JSOAuction.API.Request.Tournament;
using JSOAuction.Domain.Entities.Bids;
using JSOAuction.Domain.Entities.PlayerRegister;
using JSOAuction.Domain.Entities.TeamRegister;
using JSOAuction.Domain.Entities.Tournament;
using JSOAuction.Services.Entities.Bids;
using JSOAuction.Services.Entities.PlayerRegister;
using JSOAuction.Services.Entities.PlayersDetailsByTeam;
using JSOAuction.Services.Entities.Team;
using JSOAuction.Services.Entities.Tournament;

namespace JSOAuction.API.Infrastructure.Automapper
{
    public class TeamMappingProfile : Profile
    {
        public TeamMappingProfile()
        {
             CreateMap<TeamRegisterRequest, TeamRegisterDto>();
              CreateMap<TeamRegister, TeamRegisterDto>().ReverseMap();
            CreateMap<DeleteTeamRequest, DeleteTeamDto>();
            CreateMap<UpdateTeamRequest, UpdateTeamDto>();
            CreateMap<GetTeamDetailsByTournamentRequest, GetTeamDetailsByTournamentDto>();
            CreateMap<GetTeamDetailsByIdRequest, GetTeamDetailsByIdDto>();

        }
    }
}
