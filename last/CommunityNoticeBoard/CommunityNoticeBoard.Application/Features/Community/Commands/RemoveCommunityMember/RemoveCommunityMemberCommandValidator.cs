using CommunityNoticeBoard.Application.IRepository;
using CommunityNoticeBoard.Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityNoticeBoard.Application.Features.Community.Commands.RemoveCommunityMember
{
    public class RemoveCommunityMemberCommandValidator
        : AbstractValidator<RemoveCommunityMemberCommand>
    {
        public RemoveCommunityMemberCommandValidator(
            IGenericRepository<UserCommunity> userCommunityRepo)
        {
            RuleFor(x => x)
                .MustAsync(async (cmd, ct) =>
                {
                    var adminEntry = (await userCommunityRepo.FindAsync(
                        uc => uc.UserId == cmd.AdminUserId
                           && uc.CommunityId == cmd.CommunityId,
                        ct))
                        .FirstOrDefault();

                    if (adminEntry == null || adminEntry.Role != CommunityRole.Admin)
                        return false;

                    if (cmd.AdminUserId == cmd.TargetUserId)
                        return false;

                    var targetEntry = (await userCommunityRepo.FindAsync(
                        uc => uc.UserId == cmd.TargetUserId
                           && uc.CommunityId == cmd.CommunityId,
                        ct))
                        .FirstOrDefault();

                    if (targetEntry == null)
                        return false;

                    if (targetEntry.Role == CommunityRole.Admin)
                        return false;

                    return true;
                })
                .WithMessage("You are not allowed to remove this member.");
        }
    }
}