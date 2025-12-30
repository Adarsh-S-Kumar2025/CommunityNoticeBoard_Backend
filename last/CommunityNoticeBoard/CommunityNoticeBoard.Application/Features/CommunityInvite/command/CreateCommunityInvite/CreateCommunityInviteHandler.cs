using CommunityNoticeBoard.Application.IRepository;
using CommunityNoticeBoard.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityNoticeBoard.Application.Features.CommunityInvite.command.CreateCommunityInvite
{
    public class CreateCommunityInviteHandler
    : IRequestHandler<CreateCommunityInviteCommand, string>
    {
        private readonly IGenericRepository<CommunityNoticeBoard.Domain.Entities.CommunityInvite> _inviteRepo;
        private readonly IGenericRepository<CommunityNoticeBoard.Domain.Entities.User> _userRepo;
        private readonly IGenericRepository<UserCommunity> _userCommunityRepo;

        public CreateCommunityInviteHandler(
            IGenericRepository<CommunityNoticeBoard.Domain.Entities.CommunityInvite> inviteRepo,
            IGenericRepository<CommunityNoticeBoard.Domain.Entities.User> userRepo,
            IGenericRepository<UserCommunity> userCommunityRepo)
        {
            _inviteRepo = inviteRepo;
            _userRepo = userRepo;
            _userCommunityRepo = userCommunityRepo;
        }

        public async Task<string> Handle(
            CreateCommunityInviteCommand request,
            CancellationToken cancellationToken)
        {
            // Already member?
            var isMember = await _userCommunityRepo.Query()
                .AnyAsync(uc =>
                    uc.CommunityId == request.CommunityId &&
                    uc.User.Email == request.invitedEmail,
                    cancellationToken);

            if (isMember)
                throw new Exception("User already in community");

            // Duplicate invite?
            var exists = await _inviteRepo.Query()
                .AnyAsync(i =>
                    i.CommunityId == request.CommunityId &&
                    i.InvitedEmail == request.invitedEmail &&
                    i.Status == InviteStatus.Pending,
                    cancellationToken);

            if (exists)
                throw new Exception("Invite already sent");

            var user = await _userRepo.Query()
                .FirstOrDefaultAsync(u => u.Email == request.invitedEmail, cancellationToken);
            var invite = new CommunityNoticeBoard.Domain.Entities.CommunityInvite(
               communityId: request.CommunityId,
               invitedEmail: request.invitedEmail,
               invitedByUserId: request.invitedByUserId,
               invitedUserId: user?.Id
            );

            await _inviteRepo.AddAsync(invite, cancellationToken);
            await _inviteRepo.SaveChangesAsync(cancellationToken);

            // 👉 Here send email (stub)
            // SendInviteEmail(request.Email, invite.Id);

            return "Invite sent successfully";
        }
    }

}
