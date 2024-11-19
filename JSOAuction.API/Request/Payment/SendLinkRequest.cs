namespace JSOAuction.API.Request.Payments
{
    public class SendLinkRequest
    {
        public string MobileNumber { get; set; }
        public string TournamentName { get; set; }
        public string PaymentLink { get; set; }
    }
}