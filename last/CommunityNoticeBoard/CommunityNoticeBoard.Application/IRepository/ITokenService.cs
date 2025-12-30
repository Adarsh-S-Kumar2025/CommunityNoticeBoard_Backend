using CommunityNoticeBoard.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CommunityNoticeBoard.Application.IRepository
{
    public interface ITokenService
    {
        public string GenerateAccessToken(int userId, string userEmail );
        public RefreshToken GenerateRefreshToken(int userId);
        public void SetRefreshTokenInCookies(RefreshToken refreshToken);
        public void RemoveRefreshTokenFromCookies();
    }
}