using CommunityNoticeBoard.Application.IRepository;
using CommunityNoticeBoard.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityNoticeBoard.Application.Features.CommunityInvite.command.AcceptCommunityInvite
{
    public class AcceptCommunityInviteHandler
    : IRequestHandler<AcceptCommunityInviteCommand, string>
    {
        private readonly IGenericRepository<CommunityNoticeBoard.Domain.Entities.CommunityInvite> _inviteRepo;
        private readonly IGenericRepository<UserCommunity> _userCommunityRepo;
        private readonly IGenericRepository<CommunityNoticeBoard.Domain.Entities.User> _userRepo;

        public AcceptCommunityInviteHandler(
            IGenericRepository<CommunityNoticeBoard.Domain.Entities.CommunityInvite> inviteRepo,
            IGenericRepository<UserCommunity> userCommunityRepo
            , IGenericRepository<Domain.Entities.User> userRepo
            )
        {
            _inviteRepo = inviteRepo;
            _userCommunityRepo = userCommunityRepo;
            _userRepo = userRepo;
        }

        public async Task<string> Handle(
            AcceptCommunityInviteCommand request,
            CancellationToken cancellationToken)
        {
            var user = await _userRepo.GetByIdAsync(request.UserId, cancellationToken);
            if (user == null)
                throw new Exception("User not found");

            var invite = await _inviteRepo.Query()
                .FirstOrDefaultAsync(i =>
                    i.Id == request.InviteId &&
                    i.Status == InviteStatus.Pending &&
                    i.InvitedEmail == user.Email,
                    cancellationToken);

            if (invite == null)
                throw new Exception("Invite not found");

            invite.AttachUser(user.Id);
            invite.Accept();

            await _userCommunityRepo.AddAsync(
                new UserCommunity(user.Id, invite.CommunityId, CommunityRole.Member),
                cancellationToken);

            await _inviteRepo.SaveChangesAsync(cancellationToken);
            await _userCommunityRepo.SaveChangesAsync(cancellationToken);

            return "Joined community successfully";
        }
    }

    }