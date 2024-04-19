using AutoMapper;
using JSOAuction.API.Request.Bids;
using JSOAuction.API.Request.PlayerRegister;
using JSOAuction.API.Request.Tournament;
using JSOAuction.Domain.Entities.Bids;
using JSOAuction.Domain.Entities.PlayerRegister;
using JSOAuction.Domain.Entities.Tournament;
using JSOAuction.Services.Entities.Bids;
using JSOAuction.Services.Entities.PlayerRegister;
using JSOAuction.Services.Entities.PlayersDetailsByTeam;
using JSOAuction.Services.Entities.Tournament;

namespace JSOAuction.API.Infrastructure.Automapper
{
    public class TournamentRegisterMappingProfile : Profile
    {
        public TournamentRegisterMappingProfile()
        {
             CreateMap<TournamentRegisterRequest, TournamentRegisterDto>();
              CreateMap<TournamentRegister, TournamentRegisterDto>().ReverseMap();

             CreateMap<UpdateTournamentRegisterRequest, UpdateTournamentRegisterDto>();
            CreateMap<DeleteTournamentRequest, DeleteTournamentDto>();
            CreateMap<GetByTournamentIdRequest, GetByTournamentIdDto>();
            //CreateMap<TournamentRegister, UpdateTournamentRegisterDto>().ReverseMap();
        }
    }
}
