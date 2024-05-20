using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSOAuction.Services.Entities.Groups
{
    public class GroupDetailsResponseModel
    {
        public int Id { get; set; }
        public string? GroupName { get; set; }
        public decimal? BasePrice { get; set; }
    }
}
