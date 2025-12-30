using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityNoticeBoard.Application.Features.Community.Commands.CreateCommunityCommand
{
    public class CreateCommunityCommand:IRequest<int>
    {
        public int UserId { get; set; }   
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
    }
}
