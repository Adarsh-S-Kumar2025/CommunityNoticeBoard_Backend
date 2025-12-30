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

namespace CommunityNoticeBoard.Application.Features.CommunityInvite.Query.GetInvitesByUserId
{
    public class GetInvitesByUserIdHandler
        : IRequestHandler<GetInvitesByUserIdQuery, List<CommunityInviteDto>>
    {
        private readonly IGenericRepository<CommunityNoticeBoard.Domain.Entities.CommunityInvite> _inviteRepo;
        private readonly IGenericRepository<Domain.Entities.User> _userRepo;

        public GetInvitesByUserIdHandler(
            IGenericRepository<CommunityNoticeBoard.Domain.Entities.CommunityInvite> inviteRepo,
            IGenericRepository<Domain.Entities.User> userRepo
            )
        {
            _inviteRepo = inviteRepo;
            _userRepo = userRepo;
        }

        public async Task<List<CommunityInviteDto>> Handle(
             GetInvitesByUserIdQuery request,
             CancellationToken cancellationToken)
        {
            var user = await _userRepo.GetByIdAsync(request.UserId, cancellationToken);
            if (user == null)
                throw new KeyNotFoundException("User not found");
            return await _inviteRepo.Query()
                .Where(i =>i.InvitedEmail == user.Email && i.Status == InviteStatus.Pending)
                .Include(i => i.Community)
                .Include(i => i.InvitedByUser)
                .Select(i => new CommunityInviteDto
                {
                    InviteId = i.Id,
                    CommunityId = i.CommunityId,
                    CommunityName = i.Community.Name,
                    InvitedByUserName = i.InvitedByUser.Name,

                    Status = i.Status.ToString(),
                    CreatedAt = i.CreatedAt
                })
                .OrderByDescending(i => i.CreatedAt)
                .ToListAsync(cancellationToken);

        }
    }
}