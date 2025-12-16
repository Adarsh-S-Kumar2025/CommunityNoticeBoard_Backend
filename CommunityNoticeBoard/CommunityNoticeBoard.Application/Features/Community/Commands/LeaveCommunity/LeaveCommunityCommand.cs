using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityNoticeBoard.Application.Features.Community.Commands.LeaveCommunity
{
    public class LeaveCommunityCommand : IRequest<bool>
    {
        public int UserId { get; set; }
        public int CommunityId { get; set; }
    }
}
