using CommunityNoticeBoard.Application.Dtos;
using CommunityNoticeBoard.Application.IRepository;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using appDomain = CommunityNoticeBoard.Domain.Entities;
namespace CommunityNoticeBoard.Application.Features.Auth.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResultDto>
    {
        private readonly ITokenService tokenService;
        private readonly IUserReposistory _userRepository;
        private readonly IGenericRepository<appDomain.RefreshToken> _refreshTokenRepository;

        public LoginCommandHandler(ITokenService tokenService, IUserReposistory userRepository, IGenericRepository<appDomain.RefreshToken> refreshTokenRepository)
        {
            this.tokenService = tokenService;
            this._userRepository = userRepository;
            this._refreshTokenRepository = refreshTokenRepository;
        }

        public async Task<LoginResultDto> Handle(LoginCommand request, CancellationToken cancellationToken)
        {

            appDomain.User? user = await _userRepository.GetUserByEmailAsync(request.Email, cancellationToken);
            if (user == null)
            {
                throw new UnauthorizedAccessException("This account does not exist.");
            }
            if (new PasswordHasher<appDomain.User>().VerifyHashedPassword(user, user.PasswordHash, request.Password)
                == PasswordVerificationResult.Failed)
            {
                throw new UnauthorizedAccessException("Invalid email or password.");
            }

            string accessToken = tokenService.GenerateAccessToken(user.Id, request.Email);
            appDomain.RefreshToken refreshToken = tokenService.GenerateRefreshToken(user.Id);

            tokenService.SetRefreshTokenInCookies(refreshToken);

            appDomain.RefreshToken refreshTokenEntity = new(user.Id, refreshToken.Token, refreshToken.ExpiresAt);

            await _refreshTokenRepository.AddAsync(refreshTokenEntity, cancellationToken);
            LoginResultDto result = new (Id: user.Id, Email: user.Email, Token: accessToken);


            await _refreshTokenRepository.SaveChangesAsync(cancellationToken);
            return result;
        }
    }
}