using CommunityNoticeBoard.Application.Dtos;
using CommunityNoticeBoard.Application.IRepository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityNoticeBoard.Application.Features.SavedPost.Queries.GetSavedPostsByUser
{
    public class GetSavedPostsByUserQueryHandler
        : IRequestHandler<GetSavedPostsByUserQuery, List<SavedPostDto>>
    {
        private readonly IGenericRepository<CommunityNoticeBoard.Domain.Entities.SavedPost> _savedPostRepo;

        public GetSavedPostsByUserQueryHandler(
            IGenericRepository<CommunityNoticeBoard.Domain.Entities.SavedPost> savedPostRepo)
        {
            _savedPostRepo = savedPostRepo;
        }

        public async Task<List<SavedPostDto>> Handle(GetSavedPostsByUserQuery request,CancellationToken cancellationToken)
        {
            var query = _savedPostRepo.Query().Where(sp => sp.UserId == request.UserId);
            if(!string.IsNullOrWhiteSpace(request.Title))
            {
                query = query.Where(sp => sp.Post.Title.Contains(request.Title));
            }
            if (!string.IsNullOrWhiteSpace(request.Category))
            {
                query = query.Where(sp => sp.Post.Category.Contains(request.Category));
            }
            if (!string.IsNullOrWhiteSpace(request.CommunityName))
            {
                query = query.Where(sp => sp.Post.Community.Name.Contains(request.CommunityName));
            }
            var result = await query
                .Select(sp => new SavedPostDto
                {
                    PostId = sp.Post.Id,
                    Title = sp.Post.Title,
                    Description = sp.Post.Description,
                    Category = sp.Post.Category.ToString(),
                    CreatedAt = sp.Post.CreatedAt,
                    ExpiryDate = sp.Post.ExpiryDate,

                    CommunityId = sp.Post.CommunityId,
                    CommunityName = sp.Post.Community.Name  
                })
                .OrderByDescending(p => p.CreatedAt)
                .ToListAsync(cancellationToken);

            return result;
        }

    }
}