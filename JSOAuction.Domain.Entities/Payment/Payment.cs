namespace JSOAuction.Domain.Entities.Payments
{
    public class Payments
    {
        public int Id { get; set; }
        public string PaymentId { get; set; }
        public string OrderId { get; set; }
        public string Name { get; set; }
        public DateTime CreatedOn { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; }
        public string Contact { get; set; }
        public string Email { get; set; }
        public int? PlayerRegisterId { get; set; }

    }
}
