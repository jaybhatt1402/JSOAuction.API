using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSOAuction.Domain.Entities.Bids
{
    [Table("Bids")]
    public class Bids
    {
        [Key]
        public int BidId { get; set; }
        public Guid PlayerId { get; set; }
        public Guid TeamId { get; set; }
        public Guid AuctionId { get; set; }
        public decimal BidAmount { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public DateTime? CreatedOn { get; set; }
        public Guid? CreatedBy { get; set; }
        public Guid? UpdatedBy { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
