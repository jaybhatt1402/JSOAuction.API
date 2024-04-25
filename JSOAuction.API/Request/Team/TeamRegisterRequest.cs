using JSOAuction.API.Helpers;
using Microsoft.AspNetCore.Mvc;
using System;

namespace JSOAuction.API.Request.Team
{
    [ModelBinder(typeof(JsonWithFilesFormDataModelBinder), Name = "json")]
    public class TeamRegisterRequest
    {
        public string? TeamName { get; set; }
        public string? OwnerName { get; set; }
        public string? CoachName { get; set; }
        public string? MobileNumber { get; set; }
        public string? Email { get; set; }
        public int? FoundedYear { get; set; }
        public string? TeamLogo { get; set; }
        public string? Owner { get; set; }
        public int? TournamentId { get; set; }
        
    }

    public class DeleteTeamRequest
    {
        public int? TeamId { get; set; }
    }

    [ModelBinder(typeof(JsonWithFilesFormDataModelBinder), Name = "json")]
    public class UpdateTeamRequest
    {
        public int TeamId { get; set; }
        public string? TeamName { get; set; }
        public string? OwnerName { get; set; }
        public string? CoachName { get; set; }
        public string? MobileNumber { get; set; }
        public string? Email { get; set; }
        public int? FoundedYear { get; set; }
        public string? TeamLogo { get; set; }
        public string? Owner { get; set; }
        public int? TournamentId { get; set; }

    }

}
