using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSOAuction.Utility
{
    public class AppSettings
    {
        public string Secret { get; set; }
        public string SubDomain { get; set; }
        public string JwtExpiryInMinutes { get; set; }
        public int RefreshTokenTTL { get; set; }

    }
}
