using CommunityNoticeBoard.Application.IRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityNoticeBoard.Application.Features.Post.Commands.CreatePost
{
    public class CreatePostCommandHandler : IRequestHandler<CreatePostCommand, int>
    {
        private readonly IGenericRepository<CommunityNoticeBoard.Domain.Entities.Post> _postRepo;

        public CreatePostCommandHandler(IGenericRepository<CommunityNoticeBoard.Domain.Entities.Post> postRepo)
        {
            _postRepo = postRepo;
        }

        public async Task<int> Handle(CreatePostCommand request, CancellationToken cancellationToken)
        {
            var post = new CommunityNoticeBoard.Domain.Entities.Post(
                userId: request.UserId,
                communityId: request.CommunityId,
                title: request.Title,
                description: request.Description,
                category: request.Category,
                expiryDate: request.ExpiryDate
            );

            await _postRepo.AddAsync(post, cancellationToken);
            await _postRepo.SaveChangesAsync(cancellationToken);

            return post.Id;
        }
    }
}