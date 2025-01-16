namespace JSOAuction.Services.Entities.Payments
{
    public class PaymentDto
    {
        public string OrderId { get; set; }
        public string PaymentId { get; set; }
        public string Currency { get; set; }
        public int Amount { get; set; }
        public string Contact { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
    }
}
