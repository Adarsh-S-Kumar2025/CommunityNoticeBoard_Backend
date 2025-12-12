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
        string GenerateAccessToken(User user);
        RefreshToken GenerateRefreshToken(int userId);

        /// Validates the access token and returns claims principal.
        ClaimsPrincipal? ValidateToken(string token);
    }
}