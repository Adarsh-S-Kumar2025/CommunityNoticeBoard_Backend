using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityNoticeBoard.Domain.Entities
{
    public class PostImage
    {
        public int Id { get; private set; }
        public int PostId { get; private set; }
        public string ImageUrl { get; private set; } = default!;

        public Post Post { get; private set; } = default!;
    }

}
