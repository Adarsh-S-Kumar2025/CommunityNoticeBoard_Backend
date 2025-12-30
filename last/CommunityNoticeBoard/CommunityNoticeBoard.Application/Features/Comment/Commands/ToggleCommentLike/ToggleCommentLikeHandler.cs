using CommunityNoticeBoard.Application.IRepository;
using CommunityNoticeBoard.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityNoticeBoard.Application.Features.Comment.Commands.ToggleCommentLike
{
    public class ToggleCommentLikeHandler
    : IRequestHandler<ToggleCommentLikeCommand, int>
    {
        private readonly IGenericRepository<CommentLike> _likeRepo;
        private readonly IGenericRepository<CommunityNoticeBoard.Domain.Entities.Comment> _commentRepo;

        public ToggleCommentLikeHandler(
            IGenericRepository<CommentLike> likeRepo,
            IGenericRepository<CommunityNoticeBoard.Domain.Entities.Comment> commentRepo)
        {
            _likeRepo = likeRepo;
            _commentRepo = commentRepo;
        }

        public async Task<int> Handle(
            ToggleCommentLikeCommand request,
            CancellationToken cancellationToken)
        {
            var commentExists = await _commentRepo.Query()
                .AnyAsync(c => c.Id == request.CommentId, cancellationToken);

            if (!commentExists)
                throw new Exception("Comment not found");

            var existingLike = await _likeRepo.Query()
                .FirstOrDefaultAsync(l =>
                    l.CommentId == request.CommentId &&
                    l.UserId == request.UserId,
                    cancellationToken);

            if (existingLike != null)
            {
                _likeRepo.DeleteAsync(existingLike);
            }
            else
            {
                await _likeRepo.AddAsync(
                    new CommentLike(request.CommentId, request.UserId),
                    cancellationToken);
            }

            await _likeRepo.SaveChangesAsync(cancellationToken);

            return await _likeRepo.Query()
                .CountAsync(l => l.CommentId == request.CommentId, cancellationToken);
        }
    }
}