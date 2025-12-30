using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityNoticeBoard.Application.Features.Post.Commands.PinPost
{
    public class PinPostCommandValidator : AbstractValidator<PinPostCommand>
    {
        public PinPostCommandValidator()
        {
            RuleFor(x => x.PostId).GreaterThan(0);
            RuleFor(x => x.UserId).GreaterThan(0);
        }
    }
}
