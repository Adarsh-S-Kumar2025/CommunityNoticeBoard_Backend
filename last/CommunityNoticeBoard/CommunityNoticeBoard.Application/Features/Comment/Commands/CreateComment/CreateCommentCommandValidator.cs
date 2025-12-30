using CommunityNoticeBoard.Application.IRepository;
using CommunityNoticeBoard.Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityNoticeBoard.Application.Features.Comment.Commands.CreateComment
{
    public class CreateCommentCommandValidator
        : AbstractValidator<CreateCommentCommand>
    {
        public CreateCommentCommandValidator(
            IGenericRepository<CommunityNoticeBoard.Domain.Entities.Post> postRepo,
            IGenericRepository<UserCommunity> userCommunityRepo)
        {
            RuleFor(x => x.Content)
                .NotEmpty()
                .MaximumLength(1000)
                .WithMessage("Comment cannot be empty.");

            RuleFor(x => x.PostId)
                .MustAsync(async (postId, ct) =>
                {
                    var post = await postRepo.GetByIdAsync(postId, ct);
                    return post != null && post.ExpiryDate > DateTime.UtcNow;
                })
                .WithMessage("Post does not exist or is expired.");

            RuleFor(x => x)
                .MustAsync(async (cmd, ct) =>
                {
                    var post = await postRepo.GetByIdAsync(cmd.PostId, ct);
                    if (post == null) return false;

                    var member = await userCommunityRepo.FindAsync(
                        uc => uc.UserId == cmd.UserId
                           && uc.CommunityId == post.CommunityId,
                        ct);

                    return member.Any();
                })
                .WithMessage("User must be a community member to comment.");
        }
    }
}