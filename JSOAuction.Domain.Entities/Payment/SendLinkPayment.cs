namespace JSOAuction.Domain.Entities.Payments
{
    public class SendLinkPayments
    {
        public int Id { get; set; }
        public string MobileNumber { get; set; }
        public string TournamentName { get; set; }
        public string PaymentLink { get; set; }
    }
}
