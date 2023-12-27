using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSOAuction.Services.Entities
{
    public class GenericList<T>
    {
        public int TotalCount { get; set; }
        public int PageCount { get; set; }
        public List<T> List { get; set; }
    }
}
