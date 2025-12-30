using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityNoticeBoard.Application.Features.CommunityInvite.command.AcceptCommunityInvite
{
    public class AcceptCommunityInviteCommand : IRequest<string>
    {
        public int InviteId { get; set; }
        public int UserId { get; set; }
    }
}
