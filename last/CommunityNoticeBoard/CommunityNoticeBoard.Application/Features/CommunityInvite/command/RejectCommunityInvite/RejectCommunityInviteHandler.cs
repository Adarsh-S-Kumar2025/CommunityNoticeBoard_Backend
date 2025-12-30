using CommunityNoticeBoard.Application.IRepository;
using CommunityNoticeBoard.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityNoticeBoard.Application.Features.CommunityInvite.command.RejectCommunityInvite
{
    public class RejectCommunityInviteHandler
        : IRequestHandler<RejectCommunityInviteCommand, string>
    {
        private readonly IGenericRepository<CommunityNoticeBoard.Domain.Entities.CommunityInvite> _inviteRepo;

        public RejectCommunityInviteHandler(
            IGenericRepository<CommunityNoticeBoard.Domain.Entities.CommunityInvite> inviteRepo)
        {
            _inviteRepo = inviteRepo;
        }

        public async Task<string> Handle(
            RejectCommunityInviteCommand request,
            CancellationToken cancellationToken)
        {
            var invite = await _inviteRepo.Query()
                .FirstOrDefaultAsync(i =>
                    i.Id == request.InviteId &&
                    i.InvitedUserId == request.UserId &&
                    i.Status == InviteStatus.Pending,
                    cancellationToken);

            if (invite == null)
                throw new Exception("Invite not found or already processed");

            invite.Reject();

            await _inviteRepo.SaveChangesAsync(cancellationToken);

            return "Invite rejected successfully";
        }
    }
}