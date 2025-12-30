using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityNoticeBoard.Domain.Entities
{
    public class CommentLike
    {
        public int Id { get; private set; }

        public int CommentId { get; private set; }
        public int UserId { get; private set; }

        public Comment Comment { get; private set; } = default!;
        public User User { get; private set; } = default!;

        private CommentLike() { }

        public CommentLike(int commentId, int userId)
        {
            CommentId = commentId;
            UserId = userId;
        }
    }

}