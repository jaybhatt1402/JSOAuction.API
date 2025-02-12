using JSOAuction.API.Helpers;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace JSOAuction.API.Request.PlayerRegister
{
    [ModelBinder(typeof(JsonWithFilesFormDataModelBinder), Name = "json")]
    public class SavePlayerRegisterRequest
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Gender { get; set; }
        public string? BattingStyle {  get; set; }
        public string? BowlingStyle { get; set; }
        public string? MobileNo { get; set; }
        public string? AlternativePhoneNo { get; set; }
        public string? Email { get; set; }
        public DateTime? DOB { get; set; }
        [DefaultValue(false)]
        public bool? Batsman { get; set; } 
        [DefaultValue(false)]
        public bool? Bowler { get; set; }
        [DefaultValue(false)]
        public bool? WicketKeeper { get; set; } 
        [DefaultValue(false)]
        public bool? BattingAllRounder { get; set; } 
        [DefaultValue(false)]
        public bool? BowlingAllRounder { get; set; } 
        public int? PreviousTeamId { get; set; }
        public string? LastPlayedYear { get; set; }
        public string? ProfilePicture { get; set; }
        public string? Password { get; set; }
        public int? AuctionId { get; set; }
        public string? City { get; set; }
        public int? TournamentId { get; set; }
        public string OccupationDetails { get; set; }
        public string OccupationTypes { get; set; }
        public int TShirtSizeId { get; set; }
    }

    [ModelBinder(typeof(JsonWithFilesFormDataModelBinder), Name = "json")]
    public class UpdatePlayerRequest
    {
        public int PlayerRegisterId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Gender { get; set; }
        public string? BattingStyle { get; set; }
        public string? BowlingStyle { get; set; }
        public string? MobileNo { get; set; }
        public string? AlternativePhoneNo { get; set; }
        public string? Email { get; set; }
        public DateTime? DOB { get; set; }
        [DefaultValue(false)]
        public bool? Batsman { get; set; }
        [DefaultValue(false)]
        public bool? Bowler { get; set; }
        [DefaultValue(false)]
        public bool? WicketKeeper { get; set; }
        [DefaultValue(false)]
        public bool? BattingAllRounder { get; set; }
        [DefaultValue(false)]
        public bool? BowlingAllRounder { get; set; }
        public int? PreviousTeamId { get; set; }
        public string? LastPlayedYear { get; set; }
        public string? ProfilePicture { get; set; }
        public string? Password { get; set; }
        public int? AuctionId { get; set; }
        public string? City { get; set; }
        public int? TournamentId { get; set; }
        public string OccupationDetails { get; set; }
        public string OccupationTypes { get; set; }
        public int TShirtSizeId { get; set; }
    }

    public class PlayerRequest
    {
        public List<PlayersRequest> Players { get; set; }
        public int GroupId { get; set; }
        public decimal Baseprice { get; set; }
    }

    public class PlayersRequest
    {
        public int PlayerId { get; set; }
    }
}
