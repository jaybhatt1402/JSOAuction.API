using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSOAuction.Domain.Entities.TeamRegister
{
    [Table("TeamRegister")]
    public class TeamRegister
    {
        [Key]
        public Guid TeamId { get; set; }
        public string? TeamName { get; set; }
        public string? CaptainName { get; set; }
        public string? CoachName { get; set; }
        public int? FoundedYear { get; set; }
        public int? TeamSize { get; set; }
        public decimal? RemainingBalance { get; set; }
        public decimal? MaximumBid { get; set; }
        public string? TeamLogo { get; set; }
        public string? Owner { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public DateTime? CreatedOn { get; set; }
        public Guid? CreatedBy { get; set; }
        public Guid? UpdatedBy { get; set; }
    }
}
