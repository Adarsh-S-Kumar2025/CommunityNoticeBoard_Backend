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

namespace CommunityNoticeBoard.Application.Features.Comment.Queries.GetCommentLikes
{
    public class GetCommentLikesHandler
    : IRequestHandler<GetCommentLikesQuery, CommentLikeDto>
    {
        private readonly IGenericRepository<CommentLike> _likeRepo;

        public GetCommentLikesHandler(
            IGenericRepository<CommentLike> likeRepo)
        {
            _likeRepo = likeRepo;
        }

        public async Task<CommentLikeDto> Handle(
            GetCommentLikesQuery request,
            CancellationToken cancellationToken)
        {
            var likes = _likeRepo.Query()
                .Where(l => l.CommentId == request.CommentId);

            return new CommentLikeDto
            {
                CommentId = request.CommentId,
                LikeCount = await likes.CountAsync(cancellationToken),
                LikedByCurrentUser = await likes
                    .AnyAsync(l => l.UserId == request.UserId, cancellationToken)
            };
        }
    }

}
