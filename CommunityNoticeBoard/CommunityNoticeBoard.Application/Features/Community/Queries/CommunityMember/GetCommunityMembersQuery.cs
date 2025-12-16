using CommunityNoticeBoard.Application.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityNoticeBoard.Application.Features.Community.Queries.CommunityMember
{
    public class GetCommunityMembersQuery : IRequest<List<CommunityMemberDto>>
    {
        public int CommunityId { get; set; }
        public int UserId { get; set; } // requester
    }
}
