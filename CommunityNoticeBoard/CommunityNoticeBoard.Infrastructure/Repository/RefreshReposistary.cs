using CommunityNoticeBoard.Application.IRepository;
using CommunityNoticeBoard.Domain.Entities;
using CommunityNoticeBoard.Infrastructure.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityNoticeBoard.Infrastructure.Repository
{
    public class RefreshReposistary : GenericRepository<RefreshToken>, IRefreshReposistary
    {
        public RefreshReposistary(AppDbContext context) : base(context)
        {
        }

        public async Task<RefreshToken?> GetByToken(string token)
        {
            return await _context.RefreshTokens.FirstOrDefaultAsync(rt => rt.Token == token);
        }
    }
}