using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
    namespace CommunityNoticeBoard.Domain.Entities
    {
        public class RefreshToken
        {
            public int Id { get; private set; }
            public int UserId { get; private set; }
            public User User { get; private set; }

            public string Token { get; private set; }
            public DateTime ExpiresAt { get; private set; }

            public bool IsRevoked { get; private set; } = false;

        private RefreshToken() { }

            public RefreshToken(int userId,string token, DateTime expiresAt)
            {
                Token = token;
                ExpiresAt = expiresAt;
                UserId = userId;
            }

            public void Revoke()
            {
                IsRevoked = true;
            }
        }
    }
