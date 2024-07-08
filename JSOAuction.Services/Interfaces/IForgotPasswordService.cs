using System.Threading.Tasks;
using JSOAuction.Services.Entities.ForgotPassword;

namespace JSOAuction.Services.Interfaces
{
    public interface IForgotPasswordService
    {
        Task<object> ForgotPassword(ForgotPasswordDto request);
    }
}
