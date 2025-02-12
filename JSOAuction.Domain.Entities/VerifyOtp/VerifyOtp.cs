using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JSOAuction.Domain.Entities
{
    [Table("VerifyOtp")]
    public class VerifyOtp
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Mobile { get; set; }

        [Required]
        public string Otp { get; set; }

        public bool IsVerified { get; set; }
    }
}
