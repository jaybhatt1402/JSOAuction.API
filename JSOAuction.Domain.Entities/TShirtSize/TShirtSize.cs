using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JSOAuction.Domain.Entities
{
    [Table("TShirtMaster")]
    public class TShirtSize
    {
        [Key]
        public int Id { get; set; }
        public string Size { get; set; }
        public int ShoulderSize { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}