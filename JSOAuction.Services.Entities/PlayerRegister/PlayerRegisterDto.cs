using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSOAuction.Services.Entities.PlayerRegister
{
    public class SavePlayerRegisterDto
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Gender { get; set; }
        public string? MobileNo { get; set; }
        public string? AlternativePhoneNo { get; set; }
        public string? Email { get; set; }
        public DateTime? DOB { get; set; }
        public bool? Batsmen { get; set; } 
        public bool? Bowler { get; set; }
        public bool? WicketKeeper { get; set; }
        public bool? BattingAllRounder { get; set; } 
        public bool? BowlingAllRounder { get; set; } 
        public Guid? PreviousTeamId { get; set; }
        public DateTime? LastPlayedYear { get; set; }
        public string? ProfilePicture { get; set; }
        public string? Password { get; set; }
    }
}
