using CommunityNoticeBoard.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CommunityNoticeBoard.Domain.Entities
{
    public class Post
    {
        public int Id { get; private set; }
        public int UserId { get; private set; }
        public int CommunityId { get; private set; }

        public string Title { get; private set; } = default!;
        public string Description { get; private set; } = default!;
        public PostCategory Category { get; private set; }
        public DateTime ExpiryDate { get; private set; }
        public DateTime CreatedAt { get; private set; }

        public bool IsResolved { get; private set; }

        public User User { get; private set; } = default!;
        public Community Community { get; private set; } = default!;

        public ICollection<PostImage> Images { get; private set; } = new List<PostImage>();
        public ICollection<Comment> Comments { get; private set; } = new List<Comment>();

        public ICollection<SavedPost> SavedBy { get; private set; } = new List<SavedPost>();

        public Post(int userId, int communityId, string title, string description, PostCategory category, DateTime expiryDate)
        {
            UserId = userId;
            CommunityId = communityId;
            Title = title;
            Description = description;
            Category = category;
            ExpiryDate = expiryDate;
            CreatedAt = DateTime.UtcNow;
            IsResolved = false;
        }
    }
    public enum PostCategory
    {
        Announcement = 1,
        Event = 2,
        ServiceOffered = 3,
        ServiceNeeded = 4,
        ForSale = 5,
        FreeGiveaway = 6,
        LostFound = 7,
        Recommendation = 8,
        GeneralDiscussion = 9
    }


}