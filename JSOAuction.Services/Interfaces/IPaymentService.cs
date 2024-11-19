using JSOAuction.Services.Entities.Groups;
using JSOAuction.Services.Entities.Payments;
using System.Threading.Tasks;

namespace JSOAuction.Services.Interfaces
{
    public interface IPaymentService
    {
        Task<object> SavePaymentDataAsync(PaymentDto request);
    }
}
