using AutoMapper;
using JSOAuction.API.Request.Bids;
using JSOAuction.API.Request.Groups;
using JSOAuction.API.Request.PlayerRegister;
using JSOAuction.API.Request.Tournament;
using JSOAuction.Services.Entities.Bids;
using JSOAuction.Services.Entities.Groups;
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
                for (int i = 0; i < Request.Form.Files.Count; i++)
                {
                    var file = Request.Form.Files[i];
                    if (file.Name.Contains("_banner"))
                    {
                        uploadBannerFile = file;
                    }
                    else
                    {
                        uploadLogoFile = file;
                    }
                }
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

        [HttpPost("DeleteTournament")]
        public async Task<Dictionary<string, object>> DeleteTournament([FromBody] DeleteTournamentRequest request)
        {
            var deleteTournamentDto = _mapper.Map<DeleteTournamentRequest, DeleteTournamentDto>(request);
            var result = await _tournamentRegisterService.DeleteTournament(deleteTournamentDto);
            return new Dictionary<string, object>() { { Constants.ResponseDataField, result } };
        }
        [HttpPost("UpdateTournament")]
        public async Task<Dictionary<string, object>> UpdateTournament([FromBody] UpdateTournamentRegisterRequest request)
        {

            var saveTournamentDto = _mapper.Map<UpdateTournamentRegisterRequest, UpdateTournamentRegisterDto>(request);
            IFormFile uploadBannerFile = null;
            IFormFile uploadLogoFile = null;

            if (Request.Form.Files.Count > 0)
            {
                for (int i = 0; i < Request.Form.Files.Count; i++)
                {
                    var file = Request.Form.Files[i];
                    if (file.Name.Contains("_banner"))
                    {
                        uploadBannerFile = file;
                    }
                    else
                    {
                        uploadLogoFile = file;
                    }
                }
            }

            saveTournamentDto.UploadBannerFile = uploadBannerFile;
            saveTournamentDto.UploadLogoFile = uploadLogoFile;
            var result = await _tournamentRegisterService.UpdateTournament(saveTournamentDto);
            return new Dictionary<string, object>() { { Constants.ResponseDataField, result } };
        }
        [HttpPost("GetTournamentById")]
        public async Task<Dictionary<string, object>> GetTournamentById(GetByTournamentIdRequest request)
        {
            var getTournamentDto = _mapper.Map<GetByTournamentIdRequest, GetByTournamentIdDto>(request);
            var result = await _tournamentRegisterService.GetTournamentById(getTournamentDto);
            return new Dictionary<string, object>() { { Constants.ResponseDataField, result } };
        }

        [HttpPost("GetTournamentAuctionStartStatus")]
        public async Task<Dictionary<string, object>> GetTournamentAuctionStartStatus(List<int> request)
        {
            //var getTournamentDto = _mapper.Map<GetByTournamentIdRequest, GetByTournamentIdDto>(request);
            var result = await _tournamentRegisterService.GetTournamentAuctionStartStatus(request);
            return new Dictionary<string, object>() { { Constants.ResponseDataField, result } };
        }
        [HttpPost("GetTournamentFormatById")]
        public async Task<Dictionary<string, object>> GetTournamentFormatById()
        {
            var result = await _tournamentRegisterService.GetTournamentFormatById();
            return new Dictionary<string, object>() { { Constants.ResponseDataField, result } };
        }
        [HttpPost("MatchTournamentLink")]
        public async Task<Dictionary<string, object>> MatchTournamentLink([FromBody] MatchTournamentLinkRequest request)
        {
            var matchTournamentLinkDto = _mapper.Map<MatchTournamentLinkRequest, MatchTournamentLinkDto>(request);
            var result = await _tournamentRegisterService.MatchTournamentLink(matchTournamentLinkDto);
            return new Dictionary<string, object>() { { Constants.ResponseDataField, result } };
        }
    }
}
