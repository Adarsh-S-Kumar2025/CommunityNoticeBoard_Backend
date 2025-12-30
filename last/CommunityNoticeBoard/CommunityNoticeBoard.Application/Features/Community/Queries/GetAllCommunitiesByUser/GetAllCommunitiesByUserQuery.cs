using CommunityNoticeBoard.Application.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityNoticeBoard.Application.Features.Community.Queries.GetAllCommunitiesByUser
{
    public class GetAllCommunitiesByUserQuery: IRequest<List<CommunityByUserDto>>
    {
        public int UserId { get; set; }
    }
}
