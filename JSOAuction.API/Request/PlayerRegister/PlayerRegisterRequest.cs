﻿using System.ComponentModel;

namespace JSOAuction.API.Request.PlayerRegister
{
    public class SavePlayerRegisterRequest
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Gender { get; set; }
        public string? MobileNo { get; set; }
        public string? AlternativePhoneNo { get; set; }
        public string? Email { get; set; }
        public DateTime? DOB { get; set; }
        [DefaultValue(false)]
        public bool? Batsmen { get; set; } 
        [DefaultValue(false)]
        public bool? Bowler { get; set; }
        [DefaultValue(false)]
        public bool? WicketKeeper { get; set; } 
        [DefaultValue(false)]
        public bool? BattingAllRounder { get; set; } 
        [DefaultValue(false)]
        public bool? BowlingAllRounder { get; set; } 
        public Guid? PreviousTeamId { get; set; }
        public DateTime? LastPlayedYear { get; set; }
        public string? ProfilePicture { get; set; }
        public string? Password { get; set; }
    }
}
