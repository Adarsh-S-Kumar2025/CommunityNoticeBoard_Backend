using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityNoticeBoard.Application.Features.Post.Commands.MarkAsResolved
{
    public class MarkAsResolvedCommand: IRequest<bool>
    {
        public int PostId { get; set; }
        public int UserId { get; set; }
    }
}
