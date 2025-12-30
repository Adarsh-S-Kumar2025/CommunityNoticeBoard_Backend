using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityNoticeBoard.Application.Features.Post.Commands.MarkAsResolved
{
    public class MarkPostAsResolvedCommandValidator
    : AbstractValidator<MarkAsResolvedCommand>
    {
        public MarkPostAsResolvedCommandValidator()
        {
            RuleFor(x => x.PostId)
                .GreaterThan(0)
                .WithMessage("PostId is required");

            RuleFor(x => x.UserId)
                .GreaterThan(0)
                .WithMessage("UserId is required");
        }
    }
}