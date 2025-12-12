using CommunityNoticeBoard.Application.IRepository;
using CommunityNoticeBoard.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityNoticeBoard.Application.Features.Community.Commands.DeleteCommunity
{
    public class DeleteCommunityCommandHandler : IRequestHandler<DeleteCommunityCommand,bool>
    {
        private readonly IGenericRepository<CommunityNoticeBoard.Domain.Entities.Community> _communityRepo;
        private readonly IGenericRepository<CommunityNoticeBoard.Domain.Entities.UserCommunity> _userCommunityRepo;

        public DeleteCommunityCommandHandler(
            IGenericRepository<CommunityNoticeBoard.Domain.Entities.Community> communityRepo,
            IGenericRepository<CommunityNoticeBoard.Domain.Entities.UserCommunity> userCommunityRepo)
        {
            _communityRepo = communityRepo;
            _userCommunityRepo = userCommunityRepo;
        }
        public async Task<bool> Handle(DeleteCommunityCommand request, CancellationToken cancellationToken)
        {
            // 1. Check if community exists
            var community = await _communityRepo.GetByIdAsync(request.CommunityId);
            if (community == null)
                throw new Exception("Community not found.");

            // 2. Check if user is admin
            var userCommunity = (await _userCommunityRepo.GetAllAsync())
                .FirstOrDefault(uc => uc.CommunityId == request.CommunityId && uc.UserId == request.UserId);

            if (userCommunity == null || userCommunity.Role != CommunityRole.Admin)
                throw new Exception("Only admin can delete this community.");

            // 3. Delete related UserCommunity entries
            var members = (await _userCommunityRepo.GetAllAsync())
                .Where(uc => uc.CommunityId == request.CommunityId)
                .ToList();

            foreach (var member in members)
            {
                await _userCommunityRepo.DeleteAsync(member);
            }

            // 4. Delete community
            await _communityRepo.DeleteAsync(community);

            // 5. Save changes
            await _userCommunityRepo.SaveChangesAsync();
            await _communityRepo.SaveChangesAsync();

            return true;
        }
    }
}
