using JSOAuction.Domain.Entities.User;
using JSOAuction.Services.Entities.User;
using JSOAuction.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSOAuction.Services.Interfaces
{
    public interface IJwtService
    {
        string GenerateSecurityToken(SessionDetailsDto sessionDetailsDto, AppSettings appSettings, out DateTime expiresOn);
        //public SessionDetailsDto ValidateJwtToken(string token);
        public RefreshToken GenerateRefreshToken(string ipAddress);
    }
}
