using CommunityNoticeBoard.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityNoticeBoard.Application.Features.Post.Commands.CreatePost
{
    public class CreatePostCommand : IRequest<int> // Returns the new Post Id
    {
        public int UserId { get; set; }        // Author of the post
        public int CommunityId { get; set; }   // Community where the post belongs
        public string Title { get; set; } = default!;
        public string Description { get; set; } = default!;
        public PostCategory Category { get; set; }
        public DateTime ExpiryDate { get; set; }
    }
}
