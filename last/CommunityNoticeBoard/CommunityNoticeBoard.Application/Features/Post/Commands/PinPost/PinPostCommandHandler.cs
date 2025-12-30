using CommunityNoticeBoard.Application.IRepository;
using CommunityNoticeBoard.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityNoticeBoard.Application.Features.Post.Commands.PinPost
{
    public class PinPostCommandHandler
        : IRequestHandler<PinPostCommand, bool>
    {
        private readonly IGenericRepository<CommunityNoticeBoard.Domain.Entities.Post> _postRepo;
        private readonly IGenericRepository<UserCommunity> _userCommunityRepo;

        public PinPostCommandHandler(
            IGenericRepository<CommunityNoticeBoard.Domain.Entities.Post> postRepo,
            IGenericRepository<UserCommunity> userCommunityRepo)
        {
            _postRepo = postRepo;
            _userCommunityRepo = userCommunityRepo;
        }

        public async Task<bool> Handle(
            PinPostCommand request,
            CancellationToken cancellationToken)
        {
            var post = await _postRepo.Query()
                .FirstOrDefaultAsync(p => p.Id == request.PostId, cancellationToken);

            if (post == null)
                return false;

            var isAdmin = await _userCommunityRepo.Query()
                .AnyAsync(uc =>
                    uc.UserId == request.UserId &&
                    uc.CommunityId == post.CommunityId &&
                    uc.Role == Domain.Entities.CommunityRole.Admin,
                    cancellationToken);

            if (!isAdmin)
                throw new UnauthorizedAccessException("Only admin can pin posts");

            if (request.Pin)
                post.Pin();
            else
                post.Unpin();

            await _postRepo.SaveChangesAsync();

            return true;
        }
    }
}