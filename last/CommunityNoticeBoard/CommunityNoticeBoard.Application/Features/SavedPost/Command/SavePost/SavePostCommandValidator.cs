using CommunityNoticeBoard.Application.IRepository;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityNoticeBoard.Application.Features.SavedPost.Command.SavePost
{
    public class SavePostCommandValidator : AbstractValidator<SavePostCommand>
    {
        public SavePostCommandValidator(
            IGenericRepository<CommunityNoticeBoard.Domain.Entities.User> userRepo,
            IGenericRepository<CommunityNoticeBoard.Domain.Entities.Post> postRepo,
            IGenericRepository<CommunityNoticeBoard.Domain.Entities.SavedPost> savedPostRepo)
        {
            RuleFor(x => x.UserId)
                .MustAsync(async (userId, ct) =>
                    await userRepo.GetByIdAsync(userId, ct) != null)
                .WithMessage("User does not exist.");

            RuleFor(x => x.PostId)
                .MustAsync(async (postId, ct) =>
                    await postRepo.GetByIdAsync(postId, ct) != null)
                .WithMessage("Post does not exist.");

            RuleFor(x => x)
                .MustAsync(async (cmd, ct) =>
                {
                    var exists = (await savedPostRepo.FindAsync(
                        sp => sp.UserId == cmd.UserId &&
                              sp.PostId == cmd.PostId,
                        ct)).Any();

                    return !exists;
                })
                .WithMessage("Post already saved by user.");
        }
    }
}