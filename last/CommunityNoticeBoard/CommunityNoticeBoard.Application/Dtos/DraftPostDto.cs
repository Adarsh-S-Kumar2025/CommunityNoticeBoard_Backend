using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityNoticeBoard.Application.Dtos
{
    public class DraftPostDto
    {
        public int Id { get; set; }
        public int CommunityId { get; set; }
        public string Title { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string Category { get; set; } = default!;
        public DateTime CreatedAt { get; set; }
        public DateTime? ExpiryDate { get; set; }
    }
}
