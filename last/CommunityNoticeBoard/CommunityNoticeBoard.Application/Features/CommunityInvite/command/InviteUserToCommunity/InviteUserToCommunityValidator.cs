using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityNoticeBoard.Application.Features.CommunityInvite.command.InviteUserToCommunity
{
    public class InviteUserToCommunityValidator
    : AbstractValidator<InviteUserToCommunityCommand>
    {
        public InviteUserToCommunityValidator()
        {
            RuleFor(x => x.CommunityId).NotEmpty();
            RuleFor(x => x.InvitedEmail).NotEmpty();
            RuleFor(x => x.InvitedByUserId).NotEmpty();
        }
    }
}