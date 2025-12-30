using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityNoticeBoard.Application.Dtos
{
    public class CommunityInviteDto
    {
        public int InviteId { get; set; }
        public int CommunityId { get; set; }
        public string CommunityName { get; set; } = default!;

        public string InvitedByUserName { get; set; } = default!;

        public string Status { get; set; } = default!;
        public DateTime CreatedAt { get; set; }
    }
}
