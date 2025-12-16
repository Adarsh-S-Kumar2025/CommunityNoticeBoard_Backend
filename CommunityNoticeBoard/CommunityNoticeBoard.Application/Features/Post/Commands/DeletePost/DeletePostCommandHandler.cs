using CommunityNoticeBoard.Application.IRepository;
using CommunityNoticeBoard.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityNoticeBoard.Application.Features.Post.Commands.DeletePost
{
    public class DeletePostCommandHandler
        : IRequestHandler<DeletePostCommand, bool>
    {
        private readonly IGenericRepository<CommunityNoticeBoard.Domain.Entities.Post> _postRepo;

        public DeletePostCommandHandler(
            IGenericRepository<CommunityNoticeBoard.Domain.Entities.Post> postRepo)
        {
            _postRepo = postRepo;
        }

        public async Task<bool> Handle(
            DeletePostCommand request,
            CancellationToken cancellationToken)
        {
            var post = await _postRepo.GetByIdAsync(request.PostId, cancellationToken);
            if (post == null)
                throw new Exception("Post not found");

            await _postRepo.DeleteAsync(post);
            await _postRepo.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}