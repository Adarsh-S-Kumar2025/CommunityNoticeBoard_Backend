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
    public class UserRepository : GenericRepository<User>, IUserReposistory
    {

        public UserRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<User?> GetUserByEmailAsync(string email, CancellationToken ct)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Email == email, ct);
        }
    }
}