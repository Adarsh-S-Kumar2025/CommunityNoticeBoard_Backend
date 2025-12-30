using CommunityNoticeBoard.Application.Dtos;
using CommunityNoticeBoard.Application.IRepository;
using CommunityNoticeBoard.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityNoticeBoard.Application.Features.CommunityInvite.Query.GetAvailableUsersForCommunity
{
    public class GetAvailableUsersForCommunityHandler
     : IRequestHandler<GetAvailableUsersForCommunityQuery, List<UserDto>>
    {
        private readonly IGenericRepository<CommunityNoticeBoard.Domain.Entities.User> _userRepo;
        private readonly IGenericRepository<UserCommunity> _userCommunityRepo;
        private readonly IGenericRepository<CommunityNoticeBoard.Domain.Entities.CommunityInvite> _inviteRepo;

        public GetAvailableUsersForCommunityHandler(
            IGenericRepository<CommunityNoticeBoard.Domain.Entities.User> userRepo,
            IGenericRepository<UserCommunity> userCommunityRepo,
            IGenericRepository<CommunityNoticeBoard.Domain.Entities.CommunityInvite> inviteRepo)
        {
            _userRepo = userRepo;
            _userCommunityRepo = userCommunityRepo;
            _inviteRepo = inviteRepo;
        }

        public async Task<List<UserDto>> Handle(
            GetAvailableUsersForCommunityQuery request,
            CancellationToken cancellationToken)
        {
            // 🔐 Admin check
            var isAdmin = await _userCommunityRepo.Query()
                .AnyAsync(uc =>
                    uc.CommunityId == request.CommunityId &&
                    uc.UserId == request.AdminUserId &&
                    uc.Role == CommunityRole.Admin,
                    cancellationToken);

            if (!isAdmin)
                throw new UnauthorizedAccessException("Admin access only");

            // 📌 Users already in community
            var memberIds = _userCommunityRepo.Query()
                .Where(uc => uc.CommunityId == request.CommunityId)
                .Select(uc => uc.UserId);

            // 📌 Users already invited
            var invitedUserIds = _inviteRepo.Query()
                .Where(i =>
                    i.CommunityId == request.CommunityId &&
                    i.Status == InviteStatus.Pending)
                .Select(i => i.InvitedUserId);

            // ✅ Available users
            var users = await _userRepo.Query()
                .Where(u =>
                    !memberIds.Contains(u.Id) &&
                    !invitedUserIds.Contains(u.Id))
                .Select(u => new UserDto
                {
                    UserId = u.Id,
                    Name = u.Name,
                    Email = u.Email
                })
                .OrderBy(u => u.Name)
                .ToListAsync(cancellationToken);

            return users;
        }
    }

}
