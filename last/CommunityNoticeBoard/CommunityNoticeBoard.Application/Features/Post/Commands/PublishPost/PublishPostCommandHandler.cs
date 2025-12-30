using CommunityNoticeBoard.Application.IRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityNoticeBoard.Application.Features.Post.Commands.PublishPost
{
    public class PublishPostCommandHandler
    : IRequestHandler<PublishPostCommand>
    {
        private readonly IGenericRepository<CommunityNoticeBoard.Domain.Entities.Post> _postRepo;

        public PublishPostCommandHandler(IGenericRepository<CommunityNoticeBoard.Domain.Entities.Post> postRepo)
        {
            _postRepo = postRepo;
        }

        public async Task Handle(PublishPostCommand request,CancellationToken cancellationToken)
        {
            var post = await _postRepo.GetByIdAsync(request.PostId, cancellationToken);

            if (post == null)
                throw new Exception("Post not found");

            if (post.UserId != request.UserId)
                throw new UnauthorizedAccessException();

            post.Publish();

            await _postRepo.SaveChangesAsync(cancellationToken);
        }
    }
}