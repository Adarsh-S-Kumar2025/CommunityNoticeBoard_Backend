using CommunityNoticeBoard.Application.Dtos;
using CommunityNoticeBoard.Application.IRepository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityNoticeBoard.Application.Features.Post.Queries.GetPostsByCommunityId
{
    public class GetPostsByCommunityIdHandler : IRequestHandler<GetPostsByCommunityIdQuery, List<PostDto>>
    {
        private readonly IGenericRepository<CommunityNoticeBoard.Domain.Entities.Post> _postRepo;

        public GetPostsByCommunityIdHandler(IGenericRepository<CommunityNoticeBoard.Domain.Entities.Post> postRepo)
        {
            _postRepo = postRepo;
        }

        public async Task<List<PostDto>> Handle(GetPostsByCommunityIdQuery request, CancellationToken cancellationToken)
        {
            // Use Query() to allow Include of User entity
            var posts = await _postRepo.Query() // returns IQueryable<Post>
                .Where(p => p.CommunityId == request.CommunityId && p.ExpiryDate > DateTime.UtcNow)
                .Include(p => p.User)
                .OrderByDescending(p => p.CreatedAt)
                .Select(p => new PostDto
                {
                    Id = p.Id,
                    UserId = p.UserId,
                    UserName = p.User.Name,
                    Title = p.Title,
                    Description = p.Description,
                    Category = p.Category,
                    CreatedAt = p.CreatedAt,
                    ExpiryDate = p.ExpiryDate,
                    IsResolved = p.IsResolved
                })
                .ToListAsync();

            return posts;
        }
    }
}