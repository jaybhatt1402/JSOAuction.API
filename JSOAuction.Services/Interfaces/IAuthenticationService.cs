using JSOAuction.Services.Entities.Login;
using JSOAuction.Services.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSOAuction.Services.Interfaces
{
    public interface IAuthenticationService
    {
        Task<LoginDto> AuthenticateAsync(UserAuthRequestDto request, string ipAddress);

    }
}
