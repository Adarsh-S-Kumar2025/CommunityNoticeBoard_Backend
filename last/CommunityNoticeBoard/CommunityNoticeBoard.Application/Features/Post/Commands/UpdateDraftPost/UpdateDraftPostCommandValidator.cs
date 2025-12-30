using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityNoticeBoard.Application.Features.Post.Commands.UpdateDraftPost
{
    public class UpdateDraftPostCommandValidator
    : AbstractValidator<UpdateDraftPostCommand>
    {
        public UpdateDraftPostCommandValidator()
        {
            RuleFor(x => x.Title).NotEmpty();
            RuleFor(x => x.Description).NotEmpty();
            RuleFor(x => x.Category).NotEmpty();
            RuleFor(x => x.ExpiryDate).NotEmpty();
        }
    }
}