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

namespace CommunityNoticeBoard.Application.Features.Post.Queries.GetPostsByCommunityId
{
    public class GetPostsByCommunityIdHandler : IRequestHandler<GetPostsByCommunityIdQuery, List<PostDto>>
    {
        private readonly IGenericRepository<CommunityNoticeBoard.Domain.Entities.Post> _postRepo;
        private readonly IGenericRepository<CommunityNoticeBoard.Domain.Entities.SavedPost> _savedPostRepo;

        public GetPostsByCommunityIdHandler(IGenericRepository<CommunityNoticeBoard.Domain.Entities.Post> postRepo, IGenericRepository<CommunityNoticeBoard.Domain.Entities.SavedPost> savedPostRepo)
        {
            _postRepo = postRepo;
            _savedPostRepo = savedPostRepo;
        }

        public async Task<List<PostDto>> Handle(GetPostsByCommunityIdQuery request, CancellationToken cancellationToken)
        {
            var query = _postRepo.Query().Where(p => p.CommunityId == request.CommunityId && !p.IsDraft && p.ExpiryDate > DateTime.UtcNow);

            if (!string.IsNullOrWhiteSpace(request.Title))
            {
                query = query.Where(p =>
                    p.Title.Contains(request.Title));
            }
            if (!string.IsNullOrWhiteSpace(request.Category))
            {
                query = query.Where(p =>
                    p.Category.Contains(request.Category));
            }
            var posts = await query
                .Include(p => p.User)
                .OrderByDescending(p => p.IsPinned)
                .ThenByDescending(p => p.CreatedAt)
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
                    IsResolved = p.IsResolved,
                    IsPinned = p.IsPinned,
                    IsSaved = _savedPostRepo.Query()
                .Any(sp =>
                    sp.PostId == p.Id &&
                    sp.UserId == request.UserId)
                })
                .ToListAsync(cancellationToken);

            return posts;
        }
    }
}