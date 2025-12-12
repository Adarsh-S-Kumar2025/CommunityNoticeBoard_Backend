using CommunityNoticeBoard.Application.IRepository;
using CommunityNoticeBoard.Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityNoticeBoard.Application.Features.Community.Commands.JoinCommunity
{
    public class JoinCommunityCommandValidator : AbstractValidator<JoinCommunityCommand>
    {
        private readonly IGenericRepository<CommunityNoticeBoard.Domain.Entities.Community> _communityRepo;
        private readonly IGenericRepository<UserCommunity> _userCommunityRepo;

        public JoinCommunityCommandValidator(
            IGenericRepository<CommunityNoticeBoard.Domain.Entities.Community> communityRepo,
            IGenericRepository<UserCommunity> userCommunityRepo)
        {
            _communityRepo = communityRepo;
            _userCommunityRepo = userCommunityRepo;

            RuleFor(x => x.CommunityId)
                .NotEmpty().WithMessage("CommunityId is required.")
                .MustAsync(async (communityId, cancellation) =>
                {
                    var community = await _communityRepo.GetByIdAsync(communityId, cancellation);
                    return community != null;
                }).WithMessage("Community does not exist.");

            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("UserId is required.")
                .MustAsync(async (command, userId, cancellation) =>
                {
                    var exists = await _userCommunityRepo
                        .FindAsync(uc => uc.CommunityId == command.CommunityId && uc.UserId == userId, cancellation);
                    return !exists.Any();
                }).WithMessage("User is already a member of this community.");
        }
    }
}