using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityNoticeBoard.Domain.Entities
{
    public class SavedPost
    {
        public int Id { get; set; }             // separate PK
        public int UserId { get; private set; }
        public int PostId { get; private set; }

        public User User { get; private set; } = default!;
        public Post Post { get; private set; } = default!;

        private SavedPost() { }

        public SavedPost(int userId, int postId)
        {
            UserId = userId;
            PostId = postId;
        }
    }
}