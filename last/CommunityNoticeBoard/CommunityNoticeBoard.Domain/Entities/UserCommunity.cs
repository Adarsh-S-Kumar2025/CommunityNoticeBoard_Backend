using CommunityNoticeBoard.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityNoticeBoard.Domain.Entities
{
    public class UserCommunity
    {
        public int Id { get; private set; }
        public int? UserId { get; private set; }
        public int CommunityId { get; private set; }
        public CommunityRole Role { get; private set; }

        public User? User { get; private set; } = default!;
        public Community Community { get; private set; } = default!;
        private UserCommunity() { }

        public UserCommunity(int? userId, int communityId, CommunityRole role)
        {
            UserId = userId;
            CommunityId = communityId;
            Role = role;
        }
    }

    public enum CommunityRole
    {
        Member = 0,
        Admin = 1
    }

}