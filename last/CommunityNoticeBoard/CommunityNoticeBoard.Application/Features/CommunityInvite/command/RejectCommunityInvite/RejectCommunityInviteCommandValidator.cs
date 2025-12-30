using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityNoticeBoard.Application.Features.CommunityInvite.command.RejectCommunityInvite
{
    public class RejectCommunityInviteCommandValidator
        : AbstractValidator<RejectCommunityInviteCommand>
    {
        public RejectCommunityInviteCommandValidator()
        {
            RuleFor(x => x.InviteId)
                .GreaterThan(0);

            RuleFor(x => x.UserId)
                .GreaterThan(0);
        }
    }
}

