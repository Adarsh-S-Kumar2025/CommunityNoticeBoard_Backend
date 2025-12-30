using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityNoticeBoard.Application.Features.SavedPost.Command.UnsavePost
{
    public class UnsavePostCommand : IRequest<bool>
    {
        public int UserId { get; set; }
        public int PostId { get; set; }
    }
}