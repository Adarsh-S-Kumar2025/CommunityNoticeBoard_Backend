using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityNoticeBoard.Application.Dtos
{
    public class CommentLikeDto
    {
        public int CommentId { get; set; }
        public int LikeCount { get; set; }
        public bool LikedByCurrentUser { get; set; }
    }
}
