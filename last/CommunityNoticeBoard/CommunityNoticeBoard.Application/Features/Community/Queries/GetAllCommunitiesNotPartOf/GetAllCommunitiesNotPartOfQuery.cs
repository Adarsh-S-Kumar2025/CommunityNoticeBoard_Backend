using CommunityNoticeBoard.Application.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityNoticeBoard.Application.Features.Community.Queries.GetAllCommunitiesNotPartOf
{
    public class GetAllCommunitiesNotPartOfQuery : IRequest<List<CommunityDto>>
    {
        
        public int UserId { get; set; }

    }
}
