using CommunityNoticeBoard.Application.Dtos;
using CommunityNoticeBoard.Application.IRepository;
using CommunityNoticeBoard.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityNoticeBoard.Application.Features.Community.Queries.GetCommunityById
{
    public class GetCommunityByIdQueryHandler
        : IRequestHandler<GetCommunityByIdQuery, CommunityDetailsDto>
    {
        private readonly IGenericRepository<CommunityNoticeBoard.Domain.Entities.Community> _communityRepo;
        private readonly IGenericRepository<UserCommunity> _userCommunityRepo;

        public GetCommunityByIdQueryHandler(
            IGenericRepository<CommunityNoticeBoard.Domain.Entities.Community> communityRepo,
            IGenericRepository<UserCommunity> userCommunityRepo)
        {
            _communityRepo = communityRepo;
            _userCommunityRepo = userCommunityRepo;
        }

        public async Task<CommunityDetailsDto> Handle(
            GetCommunityByIdQuery request,
            CancellationToken cancellationToken)
        {
            // 1. Fetch community
            var community = await _communityRepo
                .Query()
                .Where(c => c.Id == request.CommunityId)
                .Select(c => new
                {
                    c.Id,
                    c.Name,
                    c.Description
                })
                .FirstOrDefaultAsync(cancellationToken);

            if (community == null)
                throw new Exception("Community not found");

            // 2. Member count
            var memberCount = (await _userCommunityRepo
                .FindAsync(uc => uc.CommunityId == request.CommunityId, cancellationToken))
                .Count();

            // 3. User role (if UserId provided)
            string? userRole = null;
            if (request.UserId.HasValue)
            {
                var userCommunity = (await _userCommunityRepo.FindAsync(
                    uc => uc.UserId == request.UserId
                       && uc.CommunityId == request.CommunityId,
                    cancellationToken))
                    .FirstOrDefault();

                userRole = userCommunity?.Role.ToString();
            }

            return new CommunityDetailsDto
            {
                Id = community.Id,
                Name = community.Name,
                Description = community.Description,
                MemberCount = memberCount,
                UserRole = userRole
            };
        }
    }
}