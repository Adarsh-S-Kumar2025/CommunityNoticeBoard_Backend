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

namespace CommunityNoticeBoard.Application.Features.Post.Queries.GetExpiredPostsByCommunityAdmin
{
    public class GetExpiredPostsByCommunityAdminHandler
        : IRequestHandler<GetExpiredPostsByCommunityAdminQuery, List<PostDto>>
    {
        private readonly IGenericRepository<CommunityNoticeBoard.Domain.Entities.Post> _postRepo;
        private readonly IGenericRepository<CommunityNoticeBoard.Domain.Entities.UserCommunity> _userCommunityRepo;

        public GetExpiredPostsByCommunityAdminHandler(
            IGenericRepository<CommunityNoticeBoard.Domain.Entities.Post> postRepo,
            IGenericRepository<CommunityNoticeBoard.Domain.Entities.UserCommunity> userCommunityRepo)
        {
            _postRepo = postRepo;
            _userCommunityRepo = userCommunityRepo;
        }

        public async Task<List<PostDto>> Handle(
            GetExpiredPostsByCommunityAdminQuery request,
            CancellationToken cancellationToken)
        {
            // 1. Check if user is Admin in this community
            var userCommunity = (await _userCommunityRepo.FindAsync(
                uc => uc.UserId == request.UserId && uc.CommunityId == request.CommunityId))
                .FirstOrDefault();

            if (userCommunity == null || userCommunity.Role != CommunityRole.Admin)
                throw new Exception("Only community admin can view expired posts.");

            var expiredPosts = await _postRepo.Query()
                .Where(p => p.CommunityId == request.CommunityId
                        && p.ExpiryDate <= DateTime.UtcNow)
                .Include(p => p.User)
                .OrderByDescending(p => p.ExpiryDate)
                .ToListAsync(cancellationToken);

            return expiredPosts.Select(p => new PostDto
            {
                Id = p.Id,
                Title = p.Title,
                Description = p.Description,
                Category = p.Category,
                ExpiryDate = p.ExpiryDate,
                UserName = p.User.Name,
            }).ToList();
        }
    }
}