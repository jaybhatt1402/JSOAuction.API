
using JSOAuction.Data.Contexts;
using JSOAuction.Data.Infrastructure;
using JSOAuction.Services.Entities.Login;
using JSOAuction.Services.Entities.User;
using JSOAuction.Services.Interfaces;
using JSOAuction.Utility;
using JSOAuction.Utility.Exceptions;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSOAuction.Services.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUnitOfWork<ReadOnlyApplicationDbContext> _readOnlyUnitOfWork;
        private readonly IUnitOfWork<ReadWriteApplicationDbContext> _readWriteUnitOfWork;
        private readonly IJwtService _jwtService;
        private readonly AppSettings _appSettings;

        public AuthenticationService(
             IUnitOfWork<ReadOnlyApplicationDbContext> readOnlyUnitOfWork,
             IUnitOfWork<ReadWriteApplicationDbContext> readWriteUnitOfWork,
             IJwtService jwtService,
             IOptions<AppSettings> appSettings)
        {
            this._readOnlyUnitOfWork = readOnlyUnitOfWork;
            this._jwtService = jwtService;
            this._appSettings = appSettings.Value;
            this._readWriteUnitOfWork = readWriteUnitOfWork;
        }

        public async Task<LoginDto> AuthenticateAsync(UserAuthRequestDto request, string ipAddress)
        {
            //var hashPassword = GenericMethods.GetHash(request.Password);
            //var user = await _readOnlyUnitOfWork.UserRegisterRepository.GetFirstOrDefaultAsync(x => x.EmailAddress == request.EmailId && x.Password == hashPassword);
            //if (user == null)
            //    throw new BadResultException("Email and Password Not valid");

            //LoginDto loginDto = new LoginDto();
            //loginDto.Id = user.UserId;
            //loginDto.FirstName = user.FirstName;
            //loginDto.LastName = user.LastName;
            //loginDto.EmailId = user.EmailAddress;
            //loginDto.Mobile = user.MobileNo;
            //loginDto.UserType = user.UserType;
            //loginDto.JwtToken = _jwtService.GenerateSecurityToken(new SessionDetailsDto
            //{
            //    FirstName = loginDto.FirstName,
            //    LastName = loginDto.LastName,
            //    UserId = loginDto.Id
            //}, _appSettings, out var expiresOn);

            //var refreshToken = _jwtService.GenerateRefreshToken(ipAddress);
            //refreshToken.UserId = loginDto.Id;
            //loginDto.RefreshToken = refreshToken.Token;
            //var isRefTokenExist = await _readOnlyUnitOfWork.RefreshTokenRepository.AnyAsync(x => x.UserId == user.UserId);
            //if (isRefTokenExist)
            //{
            //    // remove old refresh tokens from user
            //    RemoveOldRefreshTokens(user.UserId);
            //    //await _readWriteUnitOfWork.RefreshTokenRepository.AttachUpdateEntity(refreshToken);
            //}
            //else
            //{
            //    await _readWriteUnitOfWork.RefreshTokenRepository.AddAsync(refreshToken);
            //}
            //await _readWriteUnitOfWork.CommitAsync();
            //var account = await _readOnlyUnitOfWork.AccountsRepository.GetFirstOrDefaultAsync(x => x.Name == username);
            //var account = await _masterDBContext.AccountsRepository.GetFirstOrDefaultAsync(x => x.Name == request.EmailId);
            //return loginDto;
            return null;
        }

        private void RemoveOldRefreshTokens(Guid UserId)
        {
            // remove old inactive refresh tokens from user based on TTL in app settings
            var data = _readWriteUnitOfWork.RefreshTokenRepository.GetAll(x => x.UserId == UserId).ToList();
            data.RemoveAll(x =>
                !x.IsActive &&
                x.Created.AddDays(_appSettings.RefreshTokenTTL) <= DateTime.UtcNow);
        }
    }
}
