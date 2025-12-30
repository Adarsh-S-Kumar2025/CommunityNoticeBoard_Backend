using CommunityNoticeBoard.Application.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityNoticeBoard.Application.Features.Comment.Queries.GetCommentLikes
{
    public class GetCommentLikesQuery : IRequest<CommentLikeDto>
    {
        public int CommentId { get; set; }
        public int UserId { get; set; }
    }
}
