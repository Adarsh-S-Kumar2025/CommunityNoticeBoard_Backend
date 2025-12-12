using CommunityNoticeBoard.Application.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityNoticeBoard.Application.Features.Post.Queries.GetExpiredPostsByCommunityAdmin
{
    public class GetExpiredPostsByCommunityAdminQuery : IRequest<List<PostDto>>
    {
        public int UserId { get; set; }
        public int CommunityId { get; set; }
    }
}
