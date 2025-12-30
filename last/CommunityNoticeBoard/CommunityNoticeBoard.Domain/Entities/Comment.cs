using CommunityNoticeBoard.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityNoticeBoard.Domain.Entities
{
    public class Comment
    {
        public int Id { get; private set; }
        public int PostId { get; private set; }
        public int UserId { get; private set; }
        public string Content { get; private set; } = default!;

        public DateTime CreatedAt { get; private set; }

        public User User { get; private set; } = default!;
        public Post Post { get; private set; } = default!;

        public ICollection<CommentLike> Likes { get; private set; } = new List<CommentLike>();

        private Comment() { }

        public Comment(int postId, int userId, string content)
        {
            PostId = postId;
            UserId = userId;
            Content = content;
        }
    }


}