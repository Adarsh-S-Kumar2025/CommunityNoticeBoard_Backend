using CommunityNoticeBoard.Application.IRepository;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityNoticeBoard.Application.Features.Post.Commands.CreatePost
{
    public class CreatePostCommandValidator : AbstractValidator<CreatePostCommand>
    {
        public CreatePostCommandValidator(
            IGenericRepository<CommunityNoticeBoard.Domain.Entities.UserCommunity> userCommunityRepo,
            IGenericRepository<CommunityNoticeBoard.Domain.Entities.Community> communityRepo)
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(200).WithMessage("Title must be less than 200 characters.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required.");

            RuleFor(x => x.CommunityId)
                .NotEmpty().WithMessage("CommunityId is required.")
                .MustAsync(async (communityId, cancellation) =>
                {
                    var community = await communityRepo.GetByIdAsync(communityId, cancellation);
                    return community != null;
                }).WithMessage("Community does not exist.");

            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("UserId is required.")
                .MustAsync(async (command, userId, cancellation) =>
                {
                    var isMember = await userCommunityRepo
                        .FindAsync(uc => uc.UserId == userId && uc.CommunityId == command.CommunityId, cancellation);
                    return isMember.Any();
                }).WithMessage("User must be a member of the community to create a post.");

            RuleFor(x => x.ExpiryDate)
                .GreaterThan(DateTime.Now).WithMessage("Expiry date must be in the future.");
        }
    }
}