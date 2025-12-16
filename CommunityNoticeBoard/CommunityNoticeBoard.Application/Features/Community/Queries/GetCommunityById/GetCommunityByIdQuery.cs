using CommunityNoticeBoard.Application.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityNoticeBoard.Application.Features.Community.Queries.GetCommunityById
{
    public class GetCommunityByIdQuery : IRequest<CommunityDetailsDto>
    {
        public int CommunityId { get; set; }
        public int? UserId { get; set; } // optional
    }
}