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
        public string Category { get; private set; }
        public DateTime ExpiryDate { get; private set; }
        public DateTime CreatedAt { get; private set; }

        public bool IsResolved { get; private set; }
        public bool IsPinned { get; private set; }
        public bool IsDraft { get; private set; }
        public User User { get; private set; } = default!;
        public Community Community { get; private set; } = default!;

        public ICollection<Comment> Comments { get; private set; } = new List<Comment>();

        public ICollection<SavedPost> SavedBy { get; private set; } = new List<SavedPost>();

        public Post(int userId, int communityId, string title, string description, string category, DateTime expiryDate,bool isDraft)
        {
            UserId = userId;
            CommunityId = communityId;
            Title = title;
            Description = description;
            Category = category;
            ExpiryDate = expiryDate;
            CreatedAt = DateTime.UtcNow;
            IsResolved = false;
            IsDraft = isDraft;
        }
        public void UpdateDraft(string title,string description,string category,DateTime expiryDate)
        {
            Title = title;
            Description = description;
            Category = category;
            ExpiryDate = expiryDate;
        }
        public void Publish()
        {
            IsDraft = false;
        }

        public void MarkAsResolved()
        {
            IsResolved = true;
        }
        public void Pin()
        {
            IsPinned = true;
        }

        public void Unpin()
        {
            IsPinned = false;
        }

       
    }
    
}