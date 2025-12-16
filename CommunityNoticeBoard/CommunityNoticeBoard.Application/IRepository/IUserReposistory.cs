using CommunityNoticeBoard.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityNoticeBoard.Application.IRepository
{
    public interface IUserReposistory : IGenericRepository<User>
    {
        Task<User?> GetUserByEmailAsync(string email, CancellationToken ct);
    }
}