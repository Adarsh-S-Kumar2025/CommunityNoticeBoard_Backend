using CommunityNoticeBoard.Application.IRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityNoticeBoard.Application.Features.Post.Commands.UpdateDraftPost
{
    public class UpdateDraftPostCommandHandler
    : IRequestHandler<UpdateDraftPostCommand>
    {
        private readonly IGenericRepository<CommunityNoticeBoard.Domain.Entities.Post> _postRepo;

        public UpdateDraftPostCommandHandler(
            IGenericRepository<CommunityNoticeBoard.Domain.Entities.Post> postRepo)
        {
            _postRepo = postRepo;
        }

        public async Task Handle(
            UpdateDraftPostCommand request,
            CancellationToken cancellationToken)
        {
            var post = await _postRepo.GetByIdAsync(request.PostId, cancellationToken);

            if (post == null)
                throw new Exception("Post not found");

            if (post.UserId != request.UserId)
                throw new UnauthorizedAccessException("Not owner");

            if (!post.IsDraft)
                throw new Exception("Only drafts can be edited");

            post.UpdateDraft(
                request.Title,
                request.Description,
                request.Category,
                request.ExpiryDate);

            await _postRepo.UpdateAsync(post);
            await _postRepo.SaveChangesAsync(cancellationToken);
        }
    }
}