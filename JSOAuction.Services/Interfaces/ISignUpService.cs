using JSOAuction.Services.Entities.SignUps;
using JSOAuction.Services.Entities.Team;
using System.Threading.Tasks;

namespace JSOAuction.Services.Interfaces
{
    public interface ISignUpService
    {
        Task<int> SignUpUser(SignUpDto request);
    }
}
