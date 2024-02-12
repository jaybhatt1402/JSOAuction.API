using AutoMapper;
using JSOAuction.API.Request.PlayerRegister;
using JSOAuction.Domain.Entities.Bids;
using JSOAuction.Domain.Entities.PlayerRegister;
using JSOAuction.Services.Entities.Bids;
using JSOAuction.Services.Entities.PlayerRegister;

namespace JSOAuction.API.Infrastructure.Automapper
{
    public class PlayerRegisterMappingProfile :Profile
    {
        public PlayerRegisterMappingProfile()
        {
           // CreateMap<GetUserRegisterRequest, GetUserRegisterDto>();
          //  CreateMap<UserRegister, GetUserRegisterDto>().ReverseMap();

            CreateMap<SavePlayerRegisterRequest, SavePlayerRegisterDto>();
            CreateMap<PlayerRegister, SavePlayerRegisterDto>().ReverseMap();

            CreateMap<SaveBidsRequest, SaveBidsDto>();
            CreateMap<Bids, SaveBidsDto>().ReverseMap();

            //  CreateMap<UpdateUserRegisterRequest, UpdateUserRegisterDto>();
            //  CreateMap<UserRegister, UpdateUserRegisterDto>().ReverseMap();

            //  CreateMap<DeleteUserRegisterRequest, DeleteUserRegisterDto>();
            //  CreateMap<UserRegister, DeleteUserRegisterDto>().ReverseMap();
        }
    }
}
