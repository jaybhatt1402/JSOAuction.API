namespace JSOAuction.Services.Entities.PlayerRegister
{
    public class PlayerRegisterResponse
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
        public bool? Batsman { get; set; }
        public bool? Bowler { get; set; }
        public bool? WicketKeeper { get; set; }
        public bool? BattingAllRounder { get; set; }
        public bool? BowlingAllRounder { get; set; }
        public int? PreviousTeamId { get; set; }
        public DateTime? LastPlayedYear { get; set; }
        public string? ProfilePicture { get; set; }
        public string? Password { get; set; }
        public decimal? BasePrice { get; set; }
        public decimal? WinningBid { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public DateTime? CreatedOn { get; set; }
        public Guid? CreatedBy { get; set; }
        public Guid? UpdatedBy { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? IsActive { get; set; }
        public string? City { get; set; }
        public string? PlayerCategory { get; set; }
        public int? PlayerNo { get; set; }
        public int? PlayerGroupId { get; set; }
        public string? TeamName { get; set; }

    }
}
