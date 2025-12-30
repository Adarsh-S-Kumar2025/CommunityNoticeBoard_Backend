using CommunityNoticeBoard.Application.IRepository;
using CommunityNoticeBoard.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
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
        private readonly IGenericRepository<CommunityNoticeBoard.Domain.Entities.Post> _postRepo;
        private readonly IGenericRepository<CommunityNoticeBoard.Domain.Entities.SavedPost> _savedPostRepo;

        public DeleteCommunityCommandHandler(
            IGenericRepository<CommunityNoticeBoard.Domain.Entities.Community> communityRepo,
            IGenericRepository<CommunityNoticeBoard.Domain.Entities.UserCommunity> userCommunityRepo,
            IGenericRepository<CommunityNoticeBoard.Domain.Entities.Post> postRepo,
            IGenericRepository<CommunityNoticeBoard.Domain.Entities.SavedPost> savedPostRepo
            )
        {
            _communityRepo = communityRepo;
            _userCommunityRepo = userCommunityRepo;
            _savedPostRepo = savedPostRepo;
            _postRepo = postRepo;
        }

        public IGenericRepository<Domain.Entities.Post> PostRepo { get; }

        public async Task<bool> Handle(DeleteCommunityCommand request, CancellationToken cancellationToken)
        {
            var community = await _communityRepo.GetByIdAsync(request.CommunityId);
            if (community == null)
                throw new Exception("Community not found.");

            //  Check if user is admin
            var userCommunity = (await _userCommunityRepo.GetAllAsync())
                .FirstOrDefault(uc => uc.CommunityId == request.CommunityId && uc.UserId == request.UserId);

            if (userCommunity == null || userCommunity.Role != CommunityRole.Admin)
                throw new Exception("Only admin can delete this community.");

            //  Delete related UserCommunity entries
            var members = (await _userCommunityRepo.GetAllAsync())
                .Where(uc => uc.CommunityId == request.CommunityId)
                .ToList();

            foreach (var member in members)
            {
                await _userCommunityRepo.DeleteAsync(member);
            }

            await _communityRepo.DeleteAsync(community);
            await _userCommunityRepo.SaveChangesAsync();
            //  Delete related SavedPost entries
            var postIds = await _postRepo.Query().Where(p => p.CommunityId == request.CommunityId).Select(p => p.Id).ToListAsync();

            var savedPosts = await _savedPostRepo.Query()
                .Where(sp => postIds.Contains(sp.PostId))
                .ToListAsync();

            foreach (var sp in savedPosts)
            {
                await _savedPostRepo.DeleteAsync(sp);
            }

            await _savedPostRepo.SaveChangesAsync();

            await _communityRepo.SaveChangesAsync();

            return true;
        }
    }
}
