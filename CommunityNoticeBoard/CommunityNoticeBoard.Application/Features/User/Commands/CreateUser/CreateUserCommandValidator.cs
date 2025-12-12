using CommunityNoticeBoard.Application.IRepository;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityNoticeBoard.Application.Features.User.Commands.CreateUser
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        private readonly IGenericRepository<CommunityNoticeBoard.Domain.Entities.User> _userRepository;

        public CreateUserCommandValidator(IGenericRepository<CommunityNoticeBoard.Domain.Entities.User> userRepository)
        {
            _userRepository = userRepository;

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MinimumLength(3).WithMessage("Name must be at least 3 characters.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.")
                .MustAsync(EmailUnique).WithMessage("Email already exists.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters.");
        }

        private async Task<bool> EmailUnique(string email, CancellationToken cancellationToken)
        {
            var existingUsers = await _userRepository.FindAsync(u => u.Email == email, cancellationToken);
            return !existingUsers.Any();
        }
    }
}