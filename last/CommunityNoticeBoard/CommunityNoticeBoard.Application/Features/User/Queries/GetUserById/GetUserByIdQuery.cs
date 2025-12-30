using CommunityNoticeBoard.Application.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityNoticeBoard.Application.Features.User.Queries.GetUserById
{
    public class GetUserByIdQuery: IRequest<UserDto>
    {
        public int UserId { get; set; }
    }
}
