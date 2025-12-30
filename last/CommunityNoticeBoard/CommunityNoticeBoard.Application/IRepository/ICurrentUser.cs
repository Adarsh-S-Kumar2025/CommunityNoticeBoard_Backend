using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityNoticeBoard.Application.IRepository
{
    public interface ICurrentUser
    {
        int UserId { get; }

        string Email { get; }

        string RefreshToken { get; }

    }
}
