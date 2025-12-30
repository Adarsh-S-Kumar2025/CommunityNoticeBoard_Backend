using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityNoticeBoard.Domain.Entities
{
    public class CommunityInvite
    {
        public int Id { get; set; }

        public int CommunityId { get; set; }
        public int? InvitedUserId { get; set; }
        public string InvitedEmail { get; private set; } = default!;

        public int InvitedByUserId { get; set; }

        public InviteStatus Status { get; set; } // Pending, Accepted, Rejected
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public Community Community { get; set; } = default!;
        public User? InvitedUser { get; set; } = default!;
        public User InvitedByUser { get; set; } = default!;
        private CommunityInvite() { }

        public CommunityInvite(int communityId, string invitedEmail, int? invitedUserId, int invitedByUserId)
        {
            CommunityId = communityId;
            InvitedEmail = invitedEmail.ToLower();
            InvitedUserId = invitedUserId;
            InvitedByUserId = invitedByUserId;
            Status = InviteStatus.Pending;
            CreatedAt = DateTime.UtcNow;
        }

        public void Accept()
        {
            if (Status != InviteStatus.Pending)
                throw new Exception("Invite is not pending");

            Status = InviteStatus.Accepted;
        }

        public void Reject()
        {
            Status = InviteStatus.Rejected;
        }
        public void AttachUser(int userId)
        {
            InvitedUserId = userId;
        }
    }

    public enum InviteStatus
    {
        Pending = 1,
        Accepted = 2,
        Rejected = 3
    }

}
