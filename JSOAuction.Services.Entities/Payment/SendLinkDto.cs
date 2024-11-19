namespace JSOAuction.Services.Entities.Payment
{
    public class SendLinkDto
    {
        public int TournamentId { get; set; }
        public string TournamentName { get; set; }
        public string PaymentLink { get; set; }
    }
}
