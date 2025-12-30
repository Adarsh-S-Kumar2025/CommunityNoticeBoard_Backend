using CommunityNoticeBoard.Application.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityNoticeBoard.Application.Features.CommunityInvite.Query.GetAvailableUsersForCommunity
{
    public class GetAvailableUsersForCommunityQuery
    : IRequest<List<UserDto>>
    {
        public int CommunityId { get; set; }
        public int AdminUserId { get; set; }
    }

}
