using JSOAuction.Domain.Entities.Groups;
using JSOAuction.Domain.Entities.Tournament;
using JSOAuction.Services.Entities.Groups;
using JSOAuction.Services.Entities.Tournament;

namespace JSOAuction.Services.Interfaces
{
    public interface ITournamentRegisterService
    {
        Task<int> SaveTournament(TournamentRegisterDto request);
        Task<List<TournamentRegister>> GetAllTournamentDetails();
        Task<bool> DeleteTournament(DeleteTournamentDto request);
        Task<string> UpdateTournament(UpdateTournamentRegisterDto request);
        Task<List<TournamentRegister>> GetTournamentById(GetByTournamentIdDto request);
    }
}
