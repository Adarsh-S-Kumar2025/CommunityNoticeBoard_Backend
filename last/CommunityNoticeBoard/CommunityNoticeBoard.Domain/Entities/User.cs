using CommunityNoticeBoard.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

    namespace CommunityNoticeBoard.Domain.Entities
    {
        public class User
        {
        public int Id { get; private set; }
        public string Name { get; private set; } = default!;
        public string Email { get; private set; } = default!;
        public string PasswordHash { get; private set; } = default!;

        public ICollection<UserCommunity> UserCommunities { get; private set; } = new List<UserCommunity>();
        public ICollection<Post> Posts { get; private set; } = new List<Post>();
        public ICollection<Comment> Comments { get; private set; } = new List<Comment>();
        public ICollection<SavedPost> SavedPosts { get; private set; } = new List<SavedPost>();
        public ICollection<RefreshToken> RefreshTokens { get; private set; } = new List<RefreshToken>();

        private User() { }

        public User(string name, string email, string passwordHash)
        {
            Name = name;
            Email = email;
            PasswordHash = passwordHash;
        }
    }
}
