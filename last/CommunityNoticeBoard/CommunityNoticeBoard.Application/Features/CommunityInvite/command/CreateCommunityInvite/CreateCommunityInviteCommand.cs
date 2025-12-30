using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityNoticeBoard.Application.Features.CommunityInvite.command.CreateCommunityInvite
{
    public class CreateCommunityInviteCommand : IRequest<string>
    {
        public int CommunityId { get; set; }
        public int invitedByUserId { get; set; }
        public string invitedEmail { get; set; } = default!;
    }
}