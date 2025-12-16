using CommunityNoticeBoard.Application.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityNoticeBoard.Application.Features.Comment.Queries.GetCommentByPost
{
    public class GetCommentsByPostIdQuery : IRequest<List<CommentDto>>
    {
        public int PostId { get; set; }
    }
}