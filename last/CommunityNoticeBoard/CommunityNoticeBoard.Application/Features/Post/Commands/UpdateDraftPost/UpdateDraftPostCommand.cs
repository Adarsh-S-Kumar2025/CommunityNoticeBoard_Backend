using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CommunityNoticeBoard.Application.Features.Post.Commands.UpdateDraftPost
{
    public class UpdateDraftPostCommand : IRequest
    {
        [JsonIgnore]
        public int PostId { get; set; }
        [JsonIgnore]
        public int UserId { get; set; }

        public string Title { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string Category { get; set; } = default!;
        public DateTime ExpiryDate { get; set; }
    }
}
