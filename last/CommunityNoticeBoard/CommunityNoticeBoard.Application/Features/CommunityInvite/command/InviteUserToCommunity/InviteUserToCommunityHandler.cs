using CommunityNoticeBoard.Application.IRepository;
using CommunityNoticeBoard.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityNoticeBoard.Application.Features.CommunityInvite.command.InviteUserToCommunity
{
    public class InviteUserToCommunityHandler
    : IRequestHandler<InviteUserToCommunityCommand, string>
    {
        private readonly IGenericRepository<UserCommunity> _userCommunityRepo;
        private readonly IGenericRepository<Domain.Entities.CommunityInvite> _inviteRepo;
        private readonly IGenericRepository<Domain.Entities.User> _userRepo;

        public InviteUserToCommunityHandler(
            IGenericRepository<UserCommunity> userCommunityRepo,
            IGenericRepository<Domain.Entities.CommunityInvite> inviteRepo,
            IGenericRepository<Domain.Entities.User> userRepo)
        {
            _userCommunityRepo = userCommunityRepo;
            _inviteRepo = inviteRepo;
            _userRepo = userRepo;
        }

        public async Task<string> Handle(
            InviteUserToCommunityCommand request,
            CancellationToken cancellationToken)
        {
            // ✅ Admin check
            var isAdmin = await _userCommunityRepo.Query()
                .AnyAsync(uc =>
                    uc.CommunityId == request.CommunityId &&
                    uc.UserId == request.InvitedByUserId &&
                    uc.Role == CommunityRole.Admin,
                    cancellationToken);

            if (!isAdmin)
                throw new UnauthorizedAccessException("Only admin can invite users");

            // ❌ User exists?
            var userExists = await _userRepo.Query()
                .AnyAsync(u => u.Id == request.InvitedUserId, cancellationToken);

            if (!userExists)
                throw new Exception("User does not exist");

            // ❌ Already member?
            var alreadyMember = await _userCommunityRepo.Query()
                .AnyAsync(uc =>
                    uc.CommunityId == request.CommunityId &&
                    uc.UserId == request.InvitedUserId,
                    cancellationToken);

            if (alreadyMember)
                throw new Exception("User already in community");

            // ❌ Already invited?
            var alreadyInvited = await _inviteRepo.Query()
                .AnyAsync(i =>
                    i.CommunityId == request.CommunityId &&
                    i.InvitedUserId == request.InvitedUserId &&
                    i.Status == InviteStatus.Pending,
                    cancellationToken);

            if (alreadyInvited)
                throw new Exception("Invite already sent");

            // ✅ Create invite
            var invite = new Domain.Entities.CommunityInvite
            (
                communityId : request.CommunityId,
                invitedEmail : request.InvitedEmail,
                invitedUserId : request.InvitedUserId,
                invitedByUserId : request.InvitedByUserId
            );

            await _inviteRepo.AddAsync(invite, cancellationToken);
            await _inviteRepo.SaveChangesAsync(cancellationToken);

            return "Invite sent successfully";
        }
    }
}