using CommunityNoticeBoard.Application.IRepository;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityNoticeBoard.Application.Features.User.Commands.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, int>
    {
        private readonly IGenericRepository<CommunityNoticeBoard.Domain.Entities.User> _userRepository;
        private readonly IPasswordHasher<CommunityNoticeBoard.Domain.Entities.User> _passwordHasher;

        public CreateUserCommandHandler(
            IGenericRepository<CommunityNoticeBoard.Domain.Entities.User> userRepository,
            IPasswordHasher<CommunityNoticeBoard.Domain.Entities.User> passwordHasher)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
        }
        public async Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var user = new CommunityNoticeBoard.Domain.Entities.User(
                name: request.Name,
                email: request.Email,
                passwordHash: ""
            );
            var hashedPassword = _passwordHasher.HashPassword(user, request.Password);

            // Assign the hashed password
            typeof(CommunityNoticeBoard.Domain.Entities.User)
                .GetProperty("PasswordHash")!
                .SetValue(user, hashedPassword);

            await _userRepository.AddAsync(user, cancellationToken);
            await _userRepository.SaveChangesAsync(cancellationToken);

            return user.Id;
        }
    }
}
