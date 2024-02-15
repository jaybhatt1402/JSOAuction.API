using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSOAuction.Domain.Entities.PlayerRegister
{
    [Table("AuctionPlayerMapping")]
    public class AuctionPlayerMapping
    {
        [Key]
        public int Id { get; set; }
        public int? PlayerId { get; set; }
        public int? AuctionId { get; set; }
        public string? PlayerStatus { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public DateTime? CreatedOn { get; set; }
        public Guid? CreatedBy { get; set; }
        public Guid? UpdatedBy { get; set; }
        public int? TeamId { get; set; }
    }
}
