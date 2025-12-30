using CommunityNoticeBoard.Application.IRepository;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityNoticeBoard.Application.Features.SavedPost.Command.UnsavePost
{
    public class UnsavePostCommandValidator
        : AbstractValidator<UnsavePostCommand>
    {
        public UnsavePostCommandValidator(
            IGenericRepository<CommunityNoticeBoard.Domain.Entities.SavedPost> savedPostRepo)
        {
            RuleFor(x => x)
                .MustAsync(async (cmd, ct) =>
                {
                    var exists = (await savedPostRepo.FindAsync(
                        sp => sp.UserId == cmd.UserId &&
                              sp.PostId == cmd.PostId,
                        ct)).Any();

                    return exists;
                })
                .WithMessage("Saved post not found.");
        }
    }
}