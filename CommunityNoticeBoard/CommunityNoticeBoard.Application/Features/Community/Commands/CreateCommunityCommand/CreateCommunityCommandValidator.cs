using CommunityNoticeBoard.Application.IRepository;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityNoticeBoard.Application.Features.Community.Commands.CreateCommunityCommand
{
    public class CreateCommunityCommandValidator : AbstractValidator<CreateCommunityCommand>
    {
        private readonly IGenericRepository<CommunityNoticeBoard.Domain.Entities.User> _userRepo;

        public CreateCommunityCommandValidator(IGenericRepository<CommunityNoticeBoard.Domain.Entities.User> userRepo)
        {
            _userRepo = userRepo;

            RuleFor(x => x.UserId)
                .GreaterThan(0)
                .WithMessage("UserId is required.")
                .MustAsync(UserExists).WithMessage("User does not exist.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Community name is required.")
                .MaximumLength(100);

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required.")
                .MaximumLength(500);
        }

        private async Task<bool> UserExists(int userId, CancellationToken cancellationToken)
        {
            var user = await _userRepo.GetByIdAsync(userId);
            return user != null;
        }
    }
}