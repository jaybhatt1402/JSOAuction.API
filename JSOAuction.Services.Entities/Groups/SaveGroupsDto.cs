using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSOAuction.Services.Entities.Groups
{
    public class SaveGroupsDto
    {
        public string? GroupName { get; set; }
        public decimal? BasePrice { get; set; }
    }

    public class DeleteGroupsDto
    {
        public int Id { get; set; }
    }
    public class UpdateGroupsDto
    {
        public int Id { get; set; }
        public string? GroupName { get; set; }
        public decimal? BasePrice { get; set; }
    }
}
