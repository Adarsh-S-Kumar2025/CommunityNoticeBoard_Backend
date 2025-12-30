using CommunityNoticeBoard.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityNoticeBoard.Application.Features.Post.Commands.CreatePost
{
    public class CreatePostCommand : IRequest<int> 
    {
        public int UserId { get; set; }        
        public int CommunityId { get; set; }   
        public string Title { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string Category { get; set; }
        public DateTime ExpiryDate { get; set; }
        public bool IsDraft { get; set; }
    }
}
