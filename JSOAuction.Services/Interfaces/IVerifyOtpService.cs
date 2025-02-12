using JSOAuction.Services.Entities;
using JSOAuction.Services.Entities.Groups;
using JSOAuction.Services.Entities.Payments;
using System.Threading.Tasks;

namespace JSOAuction.Services.Interfaces
{
    public interface IVerifyOtpService
    {
        Task<object> VerifyOtpAsync(VerifyOtpDto request);
    }
}
