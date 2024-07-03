namespace JSOAuction.API.Request.SignUps
{
    public class SignUpRequest
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? EmailId { get; set; }
        public string? Mobile { get; set; }
        public string? NewPassword { get; set; }   
        public string? ConfirmPassword { get; set; }
    }
}
