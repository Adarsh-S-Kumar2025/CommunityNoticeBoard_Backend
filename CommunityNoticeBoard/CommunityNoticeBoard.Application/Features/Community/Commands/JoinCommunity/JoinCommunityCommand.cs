using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityNoticeBoard.Application.Features.Community.Commands.JoinCommunity
{
    public class JoinCommunityCommand : IRequest<bool>
    {
        public int CommunityId { get; set; }
        public int UserId { get; set; } // user joining the community
    }
}
