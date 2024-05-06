using JSOAuction.Domain.Entities.AuctionRegister;
using JSOAuction.Domain.Entities.PlayerRegister;
using JSOAuction.Services.Entities.Auction;
using JSOAuction.Services.Entities.PlayerRegister;

namespace JSOAuction.Services.Interfaces
{
    public interface IAuctionRegisterService
    {
        Task<List<AuctionRegister>> GetAllAuctionDetails();
        Task<int> SaveAuction(SaveAuctionDto saveAuctionRequest);
        Task<int> UpdateAuction(UpdateAuctionDto updateAuctionRequest);
        Task<bool> DeleteAuction(DeleteAuctionDto request);
        Task<List<AuctionRegister>> GetAuctionById(int? AuctionId);
        Task<List<AuctionRegister>> GetAuctionDetailsByTournament(int? TournamentId);
    }
}
