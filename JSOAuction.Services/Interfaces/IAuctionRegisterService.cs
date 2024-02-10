using JSOAuction.Domain.Entities.AuctionRegister;

namespace JSOAuction.Services.Interfaces
{
    public interface IAuctionRegisterService
    {
        Task<List<AuctionRegister>> GetAllAuctionDetails();
    }
}
