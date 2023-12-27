using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSOAuction.Services.Entities.User
{
    public class UserAuthRequestDto
    {
        public string EmailId { get; set; }
        public string? Password { get; set; }
    }
}
