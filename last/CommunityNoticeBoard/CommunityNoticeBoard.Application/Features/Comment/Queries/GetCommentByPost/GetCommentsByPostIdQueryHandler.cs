using CommunityNoticeBoard.Application.Dtos;
using CommunityNoticeBoard.Application.IRepository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityNoticeBoard.Application.Features.Comment.Queries.GetCommentByPost
{
    public class GetCommentsByPostIdQueryHandler
        : IRequestHandler<GetCommentsByPostIdQuery, List<CommentDto>>
    {
        private readonly IGenericRepository<CommunityNoticeBoard.Domain.Entities.Comment> _commentRepo;
        private readonly IGenericRepository<CommunityNoticeBoard.Domain.Entities.Post> _postRepo;
        private readonly IGenericRepository<CommunityNoticeBoard.Domain.Entities.CommentLike> _commentLike;

        public GetCommentsByPostIdQueryHandler(
            IGenericRepository<CommunityNoticeBoard.Domain.Entities.Comment> commentRepo,
            IGenericRepository<CommunityNoticeBoard.Domain.Entities.Post> postRepo,
            IGenericRepository<CommunityNoticeBoard.Domain.Entities.CommentLike> CommentLike
            )
        {
            _commentRepo = commentRepo;
            _postRepo = postRepo;
            _commentLike = CommentLike;
        }

        public async Task<List<CommentDto>> Handle(
            GetCommentsByPostIdQuery request,
            CancellationToken cancellationToken)
        {
            var postExists = await _postRepo.GetByIdAsync(request.PostId, cancellationToken);
            if (postExists == null)
                throw new Exception("Post not found");

            var comments = await _commentRepo
                .Query()
                .Where(c => c.PostId == request.PostId)
                .Include(c => c.User)
                .OrderByDescending(c => c.CreatedAt)
                .Select(c => new CommentDto
                {
                    Id = c.Id,
                    UserId = c.UserId,
                    UserName = c.User.Name, 
                    Content = c.Content,
                    CreatedAt = c.CreatedAt,
                    IsLiked = _commentLike.Query()
                .Any(sp =>
                    sp.CommentId == c.Id &&
                    sp.UserId == request.UserId),
                    LikeCount = _commentLike.Query()
                    .Count(sp => sp.CommentId == c.Id)

                })
                .ToListAsync(cancellationToken);

            return comments;
        }
    }
}
