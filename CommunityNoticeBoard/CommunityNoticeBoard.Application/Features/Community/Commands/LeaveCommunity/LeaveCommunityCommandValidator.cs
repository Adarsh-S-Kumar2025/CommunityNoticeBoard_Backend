using CommunityNoticeBoard.Application.IRepository;
using CommunityNoticeBoard.Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityNoticeBoard.Application.Features.Community.Commands.LeaveCommunity
{
    public class LeaveCommunityCommandValidator
        : AbstractValidator<LeaveCommunityCommand>
    {
        public LeaveCommunityCommandValidator(
            IGenericRepository<UserCommunity> userCommunityRepo)
        {
            RuleFor(x => x)
                .MustAsync(async (cmd, ct) =>
                {
                    var memberships = await userCommunityRepo.FindAsync(
                        uc => uc.CommunityId == cmd.CommunityId,
                        ct);

                    var currentUser = memberships
                        .FirstOrDefault(uc => uc.UserId == cmd.UserId);

                    if (currentUser == null)
                        return false; // not a member

                    // If admin, ensure there is another admin
                    if (currentUser.Role == CommunityRole.Admin)
                    {
                        var adminCount = memberships
                            .Count(uc => uc.Role == CommunityRole.Admin);

                        return adminCount > 1;
                    }

                    return true;
                })
                .WithMessage("You cannot leave the community as the only admin.");
        }
    }
}