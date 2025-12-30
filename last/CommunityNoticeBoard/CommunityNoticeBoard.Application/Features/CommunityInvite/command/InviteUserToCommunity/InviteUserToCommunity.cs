using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityNoticeBoard.Application.Features.CommunityInvite.command.InviteUserToCommunity
{
    public class InviteUserToCommunityCommand : IRequest<string>
    {
        public int CommunityId { get; set; }
        public string InvitedEmail { get; set; } = string.Empty;
        public int? InvitedUserId { get; set; }
        public int InvitedByUserId { get; set; }
    }

}
