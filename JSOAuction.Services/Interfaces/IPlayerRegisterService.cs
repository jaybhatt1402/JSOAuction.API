using JSOAuction.Domain.Entities.PlayerRegister;
using JSOAuction.Domain.Entities.TeamRegister;
using JSOAuction.Services.Entities.Bids;
using JSOAuction.Services.Entities.Common;
using JSOAuction.Services.Entities.PlayerRegister;
using JSOAuction.Services.Entities.Team;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSOAuction.Services.Interfaces
{
    public interface IPlayerRegisterService
    {
        Task<int> SavePlayerRegister(SavePlayerRegisterDto request);
        Task<List<PlayerRegister>> GetAllPlayerDetails(int? AuctionId, PaginationDto paginationDto);
        Task<object> GetAuctionPlayerDetails(AuctionPlayerDto request);
        Task<bool> UpdatePlayerStatus(UpdatePlayerStatusDto request);
        Task<bool> SoldPlayer(SoldPlayerDto request);
        Task<object> SavePlayer(SavePlayerRegisterDto request);
        Task<List<PlayerRegisterResponse>> GetAllPlayerDetailsWithTournamentID(int? TournamentId, PaginationDto pagination);
        Task<byte[]> GetPlayerDetailsFileWithTournamentID(int? TournamentId);
        Task<bool> DeletePlayer(DeletePlayerDto request);
        Task<List<PlayerRegister>> GetPlayerById(int? PlayerRegisterId);
        Task<object> UpdatePlayer(UpdatePlayerDto request);
        Task<bool> AssignGroupToPlayers(PlayerRequestDto request);
    }
}
