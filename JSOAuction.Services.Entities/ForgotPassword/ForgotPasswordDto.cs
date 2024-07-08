namespace JSOAuction.Services.Entities.ForgotPassword
{
    public class ForgotPasswordDto
    {
        public string? EmailId { get; set; }
    }
        public class ResetPasswordDto
    {
        public string? ConfirmPassword { get; set; }
        public Guid? ResetPasswordToken { get; set; }

    }

}
