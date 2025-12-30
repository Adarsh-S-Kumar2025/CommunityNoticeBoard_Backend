using CommunityNoticeBoard.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityNoticeBoard.Application.Dtos
{
    public class PostDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; } = default!;
        public string Title { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string Category { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ExpiryDate { get; set; }
        public bool IsResolved { get; set; }
        public bool IsSaved { get; set; }
        public bool IsPinned { get; set; }
    }
}
