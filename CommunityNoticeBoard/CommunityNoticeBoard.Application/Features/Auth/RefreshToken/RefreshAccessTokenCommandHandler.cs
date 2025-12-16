using CommunityNoticeBoard.Application.Dtos;
using CommunityNoticeBoard.Application.IRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using appDomain = CommunityNoticeBoard.Domain.Entities;
namespace CommunityNoticeBoard.Application.Features.Auth.RefreshToken
{
    public class RefreshAccessTokenCommandHandler : IRequestHandler<RefreshAccessTokenCommand, RefreshDTO>
    {
        private readonly IRefreshReposistary _refreshTokenRepository;
        private readonly ICurrentUser _currentUser;
        private readonly IGenericRepository<appDomain.User> _userRepository;
        private readonly ITokenService _tokenService;
        public RefreshAccessTokenCommandHandler(IRefreshReposistary refreshTokenRepository, ICurrentUser currentUser, ITokenService tokenService, IGenericRepository<appDomain.User> userRepository)
        {
            _refreshTokenRepository = refreshTokenRepository;
            _currentUser = currentUser;
            _tokenService = tokenService;
            _userRepository = userRepository;
        }
        public async Task<RefreshDTO> Handle(RefreshAccessTokenCommand request, CancellationToken cancellationToken)
        {
            string refreshToken = _currentUser.RefreshToken;
            appDomain.RefreshToken? dbtoken = await _refreshTokenRepository.GetByToken(refreshToken);
            if (dbtoken is null || dbtoken.ExpiresAt < DateTime.UtcNow)
            {
                throw new UnauthorizedAccessException("Invalid or expired refresh token.");
            }
            appDomain.User? user = await _userRepository.GetByIdAsync(dbtoken.UserId, cancellationToken);

            if (user is null)
            {
                throw new UnauthorizedAccessException("User not found.");
            }

            dbtoken.Revoke();
            string newAccessToken = _tokenService.GenerateAccessToken(dbtoken.UserId, user.Email);

            appDomain.RefreshToken newRefreshToken = _tokenService.GenerateRefreshToken(dbtoken.UserId);
            _tokenService.SetRefreshTokenInCookies(newRefreshToken);
            await _refreshTokenRepository.AddAsync(newRefreshToken, cancellationToken);
            await _refreshTokenRepository.SaveChangesAsync(cancellationToken);


            RefreshDTO result = new(newAccessToken);
            return result;
        }
    }
}