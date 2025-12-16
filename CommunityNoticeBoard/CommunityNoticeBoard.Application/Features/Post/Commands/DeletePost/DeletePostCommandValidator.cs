using CommunityNoticeBoard.Application.IRepository;
using CommunityNoticeBoard.Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityNoticeBoard.Application.Features.Post.Commands.DeletePost
{
    public class DeletePostCommandValidator
        : AbstractValidator<DeletePostCommand>
    {
        public DeletePostCommandValidator(
            IGenericRepository<CommunityNoticeBoard.Domain.Entities.Post> postRepo,
            IGenericRepository<UserCommunity> userCommunityRepo)
        {
            RuleFor(x => x.PostId)
                .MustAsync(async (postId, ct) =>
                {
                    return await postRepo.GetByIdAsync(postId, ct) != null;
                })
                .WithMessage("Post not found.");

            RuleFor(x => x)
                .MustAsync(async (cmd, ct) =>
                {
                    var post = await postRepo.GetByIdAsync(cmd.PostId, ct);
                    if (post == null) return false;

                    // Owner can delete
                    if (post.UserId == cmd.UserId)
                        return true;

                    // Community admin can delete
                    var userCommunity = await userCommunityRepo.FindAsync(
                        uc => uc.UserId == cmd.UserId
                           && uc.CommunityId == post.CommunityId,
                        ct);

                    return userCommunity.Any(uc => uc.Role == CommunityRole.Admin);
                })
                .WithMessage("Only post owner or community admin can delete the post.");
        }
    }
}