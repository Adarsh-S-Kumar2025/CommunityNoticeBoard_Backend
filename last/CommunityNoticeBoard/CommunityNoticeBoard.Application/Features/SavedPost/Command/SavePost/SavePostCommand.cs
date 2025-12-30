using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityNoticeBoard.Application.Features.SavedPost.Command.SavePost
{
    public class SavePostCommand : IRequest<int>
    {
        public int UserId { get; set; }
        public int PostId { get; set; }
    }
}
