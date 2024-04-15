using AutoMapper;
using JSOAuction.API.Request.PlayerRegister;
using JSOAuction.API.Request.Tournament;
using JSOAuction.Services.Entities.PlayerRegister;
using JSOAuction.Services.Entities.PlayersDetailsByTeam;
using JSOAuction.Services.Entities.Tournament;
using JSOAuction.Services.Interfaces;
using JSOAuction.Services.Services;
using JSOAuction.Utility;
using Microsoft.AspNetCore.Mvc;

namespace JSOAuction.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TournamentController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ITournamentRegisterService _tournamentRegisterService;
        public TournamentController(ITournamentRegisterService tournamentRegisterService, IMapper mapper)
        {
            _mapper = mapper;
            _tournamentRegisterService = tournamentRegisterService;
        }
        [HttpPost("SaveTournament")]
        public async Task<Dictionary<string, object>> SaveTournament([FromBody] TournamentRegisterRequest request)
        {

            var saveTournamentDto = _mapper.Map<TournamentRegisterRequest, TournamentRegisterDto>(request);
            IFormFile uploadBannerFile = null;
            IFormFile uploadLogoFile = null;

            if (Request.Form.Files.Count > 0)
            {
                uploadBannerFile = Request.Form.Files[0];
                uploadLogoFile = Request.Form.Files[1];
            }
            saveTournamentDto.UploadBannerFile = uploadBannerFile;
            saveTournamentDto.UploadLogoFile = uploadLogoFile;
            var result = await _tournamentRegisterService.SaveTournament(saveTournamentDto);
            return new Dictionary<string, object>() { { Constants.ResponseDataField, result } };
        }
        [HttpPost("GetAllTournamentDetails")]
        public async Task<Dictionary<string, object>> GetAllTournamentDetails()
        {
            var result = await _tournamentRegisterService.GetAllTournamentDetails();
            return new Dictionary<string, object>() { { Constants.ResponseDataField, result } };
        }
    }
}
