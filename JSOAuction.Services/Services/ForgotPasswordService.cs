using AutoMapper;
using JSOAuction.Data.Contexts;
using JSOAuction.Data.Infrastructure;
using JSOAuction.Services.Entities.ForgotPassword;
using JSOAuction.Services.Interfaces;
using MailKit.Net.Smtp;
using MimeKit;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;
using JSOAuction.Utility;

namespace JSOAuction.Services.Services
{
    public class ForgotPasswordService : IForgotPasswordService
    {
        private readonly IUnitOfWork<ReadOnlyApplicationDbContext> _readOnlyUnitOfWork;
        private readonly IUnitOfWork<ReadWriteApplicationDbContext> _readWriteUnitOfWork;
        private readonly IUnitOfWork<MasterDbContext> _masterDBContext;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ForgotPasswordService(IUnitOfWork<ReadOnlyApplicationDbContext> readOnlyUnitOfWork,
                                     IUnitOfWork<MasterDbContext> masterDBContext,
                                     IMapper mapper,
                                     IUnitOfWork<ReadWriteApplicationDbContext> readWriteUnitOfWork,
                                     IHttpContextAccessor httpContextAccessor)
        {
            _readOnlyUnitOfWork = readOnlyUnitOfWork;
            _masterDBContext = masterDBContext;
            _readWriteUnitOfWork = readWriteUnitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<object> ForgotPassword(ForgotPasswordDto request)
        {
            var data = await _readWriteUnitOfWork.SignUpRepository.GetFirstOrDefaultAsync(x => x.EmailId == request.EmailId);
            if (data == null)
            {
                return "Password reset link successful";
            }

            //var resetToken = Guid.NewGuid().ToString();
            //var requestScheme = _httpContextAccessor.HttpContext.Request.Scheme;
            //var requestHost = _httpContextAccessor.HttpContext.Request.Host.Value;
            //var resetLink = $"https://{requestHost}/Account/ResetPassword?token={resetToken}";

            var resetToken = Guid.NewGuid();
            data.ResetPasswordToken = resetToken;
            await _readWriteUnitOfWork.CommitAsync();
            var resetLink = $"http://localhost:3000/update-password?token={resetToken}";

            var emailSent = await SendResetEmailAsync(request.EmailId, resetLink);
            if (!emailSent)
            {
                return "Error sending email.";
            }

            return data.ResetPasswordToken;
        }

        private async Task<bool> SendResetEmailAsync(string email, string resetLink)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("JSO", "ashishp.dcs@gmail.com"));
            message.To.Add(new MailboxAddress("", email));
            message.Subject = "Password Reset";
            message.Body = new TextPart("plain")
            {
                Text = $"Click the link to reset your password: {resetLink}"
            };

            using (var client = new SmtpClient())
            {
                try
                {
                    await client.ConnectAsync("smtp.gmail.com", 587, false);
                    await client.AuthenticateAsync("ashishp.dcs@gmail.com", "hkgo ddtd ypwv rhgp");
                    await client.SendAsync(message);
                    await client.DisconnectAsync(true);
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public async Task<object> ResetPassword(ResetPasswordDto request)
        {
            var data = await _readWriteUnitOfWork.SignUpRepository.GetFirstOrDefaultAsync(x => x.ResetPasswordToken == request.ResetPasswordToken);
            if (data == null)
            {
                return "Invalid Token";
            }

                var hashPassword = GenericMethods.GetHash(request.ConfirmPassword);
            var hash = GenericMethods.GetHash(request.NewPassword);
            data.NewPassword = hash;
            data.ConfirmPassword = hashPassword;
            await _readWriteUnitOfWork.CommitAsync();
            return data.Id;
        }
    }
}
