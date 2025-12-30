using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityNoticeBoard.Application.Features.Post.Commands.PinPost
{
    public class PinPostCommand : IRequest<bool>
    {
        public int PostId { get; set; }
        public int UserId { get; set; }
        public bool Pin { get; set; }
    }
}
