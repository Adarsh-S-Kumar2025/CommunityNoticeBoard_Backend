using CommunityNoticeBoard.Application.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityNoticeBoard.Application.Features.Post.Queries.GetPostsByCommunityId
{
    public class GetPostsByCommunityIdQuery : IRequest<List<PostDto>>
    {
        public int CommunityId { get; set; }
        public int UserId { get; set; }
        public string? Title { get; set; }
        public string? Category { get; set; }
    }
}
