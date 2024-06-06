using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSOAuction.Domain.Entities.Tournament
{
    [Table("TournamentRegister")]
    public class TournamentRegister
    {
        [Key]
        public int TournamentId { get; set; }
        public Guid TournamentGuid { get; set; }
        public string? TournamentName { get; set; }
        public string? Description { get; set; }
        public string? OrganizerName { get; set; }
        public string? OrganizerContact { get; set; }
        public string? OrganizerEmail { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? DueTime { get; set; }
        public DateTime? AuctionStartDate { get; set; }
        public string? GroundAddress { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Country { get; set; }
        public string? ZipCode { get; set; }
        public string? UploadBanner { get; set; }
        public string? UploadLogo { get; set; }
        public bool? Open { get; set; }
        public bool? Corporate { get; set; }
        public bool? Community { get; set; }
        public bool? School { get; set; }
        public bool? BoxCricket { get; set; }
        public bool? Series { get; set; }
        public bool? Other { get; set; }
        public string? BallType { get; set; }
        public string? PlayerOrderBy { get; set; }
        public int? Overs { get; set; }
        public string? Format { get; set; }
        public int? MaxTeams { get; set; }
        public string? Gender { get; set; }
        public int? MinPlayer { get; set; }
        public int? MaxPlayer { get; set; }
        public string? PaymentTerms { get; set; }
        public decimal? Amount { get; set; }
        public DateTime? CreatedOn { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public Guid? UpdatedBy { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public decimal? BidAmount { get; set; }
        public decimal? TotalBalance { get; set; }
    }
}
