using CommunityNoticeBoard.Application.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityNoticeBoard.Application.Features.CommunityInvite.Query.GetInvitesByUserId
{
    public class GetInvitesByUserIdQuery : IRequest<List<CommunityInviteDto>>
    {
        public int UserId { get; set; }
    }
}
