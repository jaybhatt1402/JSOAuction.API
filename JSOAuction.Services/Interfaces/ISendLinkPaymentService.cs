using JSOAuction.Services.Entities.Payment;
using System.Threading.Tasks;

namespace JSOAuction.Services.Interfaces
{
    public interface ISendLinkPaymentService
    {
        Task<object> SendLinkPaymentDataAsync(SendLinkDto request);
    }
}
