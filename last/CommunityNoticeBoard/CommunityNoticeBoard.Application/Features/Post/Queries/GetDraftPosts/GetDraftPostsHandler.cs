using CommunityNoticeBoard.Application.Dtos;
using CommunityNoticeBoard.Application.IRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityNoticeBoard.Application.Features.Post.Queries.GetDraftPosts
{
    public class GetDraftPostsHandler : IRequestHandler<GetDraftPostsQuery, List<DraftPostDto>>
    {
        private readonly IGenericRepository<CommunityNoticeBoard.Domain.Entities.Post> _postRepo;

        public GetDraftPostsHandler(IGenericRepository<CommunityNoticeBoard.Domain.Entities.Post> postRepo)
        {
            _postRepo = postRepo;
        }
        public async Task<List<DraftPostDto>> Handle(GetDraftPostsQuery request, CancellationToken cancellationToken)
        {
            var Posts = await _postRepo.FindAsync(p => p.UserId == request.UserId && p.CommunityId == request.CommunityId && p.IsDraft);
            var DraftPost = Posts.Select(p => new DraftPostDto
            {
                Id = p.Id,
                CommunityId=p.CommunityId,
                Title = p.Title,
                Description = p.Description,
                Category = p.Category,
                CreatedAt = p.CreatedAt,
                ExpiryDate = p.ExpiryDate
            }).OrderByDescending(p => p.CreatedAt).ToList();

            return DraftPost;

        }
    }
}
