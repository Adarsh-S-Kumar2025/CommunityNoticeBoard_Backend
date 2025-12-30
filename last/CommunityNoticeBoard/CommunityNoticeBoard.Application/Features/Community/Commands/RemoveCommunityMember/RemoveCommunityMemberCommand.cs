using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityNoticeBoard.Application.Features.Community.Commands.RemoveCommunityMember
{
    public class RemoveCommunityMemberCommand : IRequest<bool>
    {
        public int AdminUserId { get; set; }     
        public int TargetUserId { get; set; }    
        public int CommunityId { get; set; }
    }
}
