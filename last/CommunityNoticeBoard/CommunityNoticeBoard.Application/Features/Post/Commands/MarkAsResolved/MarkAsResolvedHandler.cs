using CommunityNoticeBoard.Application.IRepository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityNoticeBoard.Application.Features.Post.Commands.MarkAsResolved
{
    public class MarkAsResolvedHandler : IRequestHandler<MarkAsResolvedCommand, bool>
    {
        private readonly IGenericRepository<CommunityNoticeBoard.Domain.Entities.Post> _postRepo;
        private readonly IGenericRepository<CommunityNoticeBoard.Domain.Entities.UserCommunity> _userCommunityRepo;

        public MarkAsResolvedHandler(
            IGenericRepository<CommunityNoticeBoard.Domain.Entities.Post> postRepo,
            IGenericRepository<CommunityNoticeBoard.Domain.Entities.UserCommunity> userCommunityRepo)
        {
            _postRepo = postRepo;
            _userCommunityRepo = userCommunityRepo;
        }

        public async Task<bool> Handle(MarkAsResolvedCommand request, CancellationToken cancellationToken)
        {
            // 1️⃣ Get Post
            var post = await _postRepo.Query()
                .FirstOrDefaultAsync(p => p.Id == request.PostId, cancellationToken);

            if (post == null)
                throw new KeyNotFoundException("Post not found");

            // 2️⃣ Check if already resolved
            if (post.IsResolved)
                throw new InvalidOperationException("Post already resolved");

            // 3️⃣ If creator → allow
            if (post.UserId == request.UserId)
            {
                post.MarkAsResolved();
                await _postRepo.SaveChangesAsync();
                return true;
            }

            // 4️⃣ Otherwise check Admin role in that community
            var isAdmin = await _userCommunityRepo.Query()
                .AnyAsync(cm =>
                    cm.CommunityId == post.CommunityId &&
                    cm.UserId == request.UserId &&
                    cm.Role == Domain.Entities.CommunityRole.Admin,
                    cancellationToken);

            if (!isAdmin)
                throw new UnauthorizedAccessException(
                    "Only Admin or Post creator can resolve this post");

            // 5️⃣ Mark resolved
            post.MarkAsResolved();
            await _postRepo.SaveChangesAsync();

            return true;
        }
    }
}