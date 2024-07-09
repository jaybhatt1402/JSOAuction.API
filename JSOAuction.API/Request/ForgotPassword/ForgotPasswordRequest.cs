namespace JSOAuction.API.Request.ForgotPassword
{
    public class ForgotPasswordRequest
    {
        public string EmailId { get; set; }
    }

    public class ResetPasswordRequest
    {
        public string? NewPassword { get; set; }
        public string? ConfirmPassword { get; set; }
        public Guid? ResetPasswordToken { get; set; }

    }
}
