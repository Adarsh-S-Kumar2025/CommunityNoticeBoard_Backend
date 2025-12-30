using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityNoticeBoard.Application.Features.CommunityInvite.command.RejectCommunityInvite
{
    public class RejectCommunityInviteCommand : IRequest<string>
    {
        public int InviteId { get; set; }
        public int UserId { get; set; }
    }
}
