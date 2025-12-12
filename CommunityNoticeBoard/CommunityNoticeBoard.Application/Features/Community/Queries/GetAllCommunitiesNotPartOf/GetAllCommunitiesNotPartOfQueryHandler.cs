using CommunityNoticeBoard.Application.Dtos;
using CommunityNoticeBoard.Application.IRepository;
using CommunityNoticeBoard.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityNoticeBoard.Application.Features.Community.Queries.GetAllCommunitiesNotPartOf
{
    public class GetAllCommunitiesNotPartOfQueryHandler : IRequestHandler<GetAllCommunitiesNotPartOfQuery, List<CommunityDto>>
    {
        private readonly IGenericRepository<CommunityNoticeBoard.Domain.Entities.Community> _communityRepo;
        private readonly IGenericRepository<CommunityNoticeBoard.Domain.Entities.UserCommunity> _userCommunityRepo;

        public GetAllCommunitiesNotPartOfQueryHandler(
            IGenericRepository<CommunityNoticeBoard.Domain.Entities.Community> communityRepo,
            IGenericRepository<CommunityNoticeBoard.Domain.Entities.UserCommunity> userCommunityRepo)
        {
            _communityRepo = communityRepo;
            _userCommunityRepo = userCommunityRepo;
        }

        public async Task<List<CommunityDto>> Handle(GetAllCommunitiesNotPartOfQuery request, CancellationToken cancellationToken)
        {
            // 1. Get all community ids that the user is part of
            var userCommunities = await _userCommunityRepo.FindAsync(
                uc => uc.UserId == request.UserId, cancellationToken);

            var joinedCommunityIds = userCommunities.Select(uc => uc.CommunityId).ToList();

            // 2. Get all communities not in joinedCommunityIds
            var allCommunities = await _communityRepo.GetAllAsync(cancellationToken);

            var notJoinedCommunities = allCommunities
                .Where(c => !joinedCommunityIds.Contains(c.Id))
                .Select(c => new CommunityDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description
                })
                .ToList();

            return notJoinedCommunities;
        }
    }
}
