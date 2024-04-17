using JSOAuction.API.Helpers;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace JSOAuction.API.Request.Tournament
{

    [ModelBinder(typeof(JsonWithFilesFormDataModelBinder), Name = "json")]
    public class UpdateTournamentRegisterRequest
    {
        public int TournamentId { get; set; }
        public string? TournamentName { get; set; }
        public string? Description { get; set; }
        public string? OrganizerName { get; set; }
        public string? OrganizerContact { get; set; }
        public string? OrganizerEmail { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? DueTime { get; set; }
        public string? GroundAddress { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Country { get; set; }
        public string? ZipCode { get; set; }
        public string? UploadBanner { get; set; }
        public string? UploadLogo { get; set; }
        [DefaultValue(false)]
        public bool? Open { get; set; }
        [DefaultValue(false)]
        public bool? Corporate { get; set; }
        [DefaultValue(false)]
        public bool? Community { get; set; }
        [DefaultValue(false)]
        public bool? School { get; set; }
        [DefaultValue(false)]
        public bool? BoxCricket { get; set; }
        [DefaultValue(false)]
        public bool? Series { get; set; }
        [DefaultValue(false)]
        public bool? Other { get; set; }
        public string? BallType { get; set; }
        public int? Overs { get; set; }
        public string? Format { get; set; }
        public int? MaxTeams { get; set; }
        public string? Gender { get; set; }
        public int? MinPlayer { get; set; }
        public int? MaxPlayer { get; set; }
        public string? PaymentTerms { get; set; }
        public decimal? Amount { get; set; }
    }
}
