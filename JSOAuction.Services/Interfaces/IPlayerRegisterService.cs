using JSOAuction.Services.Entities.PlayerRegister;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSOAuction.Services.Interfaces
{
    public interface IPlayerRegisterService
    {
        Task<Guid> SavePlayerRegister(SavePlayerRegisterDto request);
    }
}
