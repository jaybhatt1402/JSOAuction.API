using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSOAuction.Services.Entities.Team
{
    public class TeamRegisterDto
    {
        public string? TeamName { get; set; }
        public string? OwnerName { get; set; }
        public string? CoachName { get; set; }
        public string? MobileNumber { get; set; }
        public string? Email { get; set; }
        public int? FoundedYear { get; set; }
        public string? TeamLogo { get; set; }
        public string? Owner { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? CreatedOn { get; set; }
        public IFormFile UploadLogoFile { get; set; }
        public int? TournamentId { get; set; }
        public decimal? TotalBalance { get; set; }

    }

    public class DeleteTeamDto
    {
        public int? TeamId { get; set; }
    }

    public class UpdateTeamDto
    {
        public int? TeamId { get; set; }
        public string? TeamName { get; set; }
        public string? OwnerName { get; set; }
        public string? CoachName { get; set; }
        public string? MobileNumber { get; set; }
        public string? Email { get; set; }
        public int? FoundedYear { get; set; }
        public string? TeamLogo { get; set; }
        public string? Owner { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? CreatedOn { get; set; }
        public IFormFile UploadLogoFile { get; set; }
        public int? TournamentId { get; set; }

    }
    public class GetTeamDetailsByIdDto
    {
        public int? TeamId { get; set; }
    }
}
