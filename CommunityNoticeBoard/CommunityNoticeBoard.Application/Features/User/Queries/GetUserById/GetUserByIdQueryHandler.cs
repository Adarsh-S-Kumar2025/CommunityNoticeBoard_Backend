using CommunityNoticeBoard.Application.Dtos;
using CommunityNoticeBoard.Application.IRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityNoticeBoard.Application.Features.User.Queries.GetUserById
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserDto>
    {
        private readonly IGenericRepository<CommunityNoticeBoard.Domain.Entities.User> _userRepository;

        public GetUserByIdQueryHandler(IGenericRepository<CommunityNoticeBoard.Domain.Entities.User> userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<UserDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user =await _userRepository.GetByIdAsync(request.UserId, cancellationToken);
            if (user == null)
                return null;

            return new UserDto
            {
                UserId = user.Id,
                Name = user.Name,
                Email = user.Email,
            };
        }
    }
}
