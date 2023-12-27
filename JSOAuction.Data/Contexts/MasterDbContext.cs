using Audit.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSOAuction.Data.Contexts
{
    [AuditDbContext(AuditEventType = "{database}")]
    public class MasterDbContext : AuditDbContext, IBaseContext
    {
        internal MasterDbContext(DbContextOptions options)
         : base(options)
        {
        }

        public MasterDbContext(DbContextOptions<MasterDbContext> options)
            : base(options)
        {
        }
    }
}
