using CommunityNoticeBoard.Application.IRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityNoticeBoard.Application.Features.SavedPost.Command.UnsavePost
{
    public class UnsavePostCommandHandler
        : IRequestHandler<UnsavePostCommand, bool>
    {
        private readonly IGenericRepository<CommunityNoticeBoard.Domain.Entities.SavedPost> _savedPostRepo;

        public UnsavePostCommandHandler(
            IGenericRepository<CommunityNoticeBoard.Domain.Entities.SavedPost> savedPostRepo)
        {
            _savedPostRepo = savedPostRepo;
        }

        public async Task<bool> Handle(
            UnsavePostCommand request,
            CancellationToken cancellationToken)
        {
            var savedPost = (await _savedPostRepo.FindAsync(
                sp => sp.UserId == request.UserId &&
                      sp.PostId == request.PostId,
                cancellationToken)).First();

           await _savedPostRepo.DeleteAsync(savedPost);
            await _savedPostRepo.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}