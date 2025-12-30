using CommunityNoticeBoard.Application.IRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityNoticeBoard.Application.Features.SavedPost.Command.SavePost
{
    public class SavePostCommandHandler
        : IRequestHandler<SavePostCommand, int>
    {
        private readonly IGenericRepository<CommunityNoticeBoard.Domain.Entities.SavedPost> _savedPostRepo;

        public SavePostCommandHandler(
            IGenericRepository<CommunityNoticeBoard.Domain.Entities.SavedPost> savedPostRepo)
        {
            _savedPostRepo = savedPostRepo;
        }

        public async Task<int> Handle(
            SavePostCommand request,
            CancellationToken cancellationToken)
        {
            var savedPost = new CommunityNoticeBoard.Domain.Entities.SavedPost(
                request.UserId,
                request.PostId);

            await _savedPostRepo.AddAsync(savedPost, cancellationToken);
            await _savedPostRepo.SaveChangesAsync(cancellationToken);

            return savedPost.Id;
        }
    }
}
