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

namespace CommunityNoticeBoard.Application.Features.Community.Queries.GetAllCommunitiesByUser
{
    internal class GetAllCommunitiesByUserQueryHandler : IRequestHandler<GetAllCommunitiesByUserQuery, List<CommunityByUserDto>>
    {
        private readonly IGenericRepository<CommunityNoticeBoard.Domain.Entities.UserCommunity> _userCommunityRepo;
        private readonly IGenericRepository<CommunityNoticeBoard.Domain.Entities.UserCommunity> _communityRepo;
        public GetAllCommunitiesByUserQueryHandler(IGenericRepository<UserCommunity> userCommunityRepo, IGenericRepository<UserCommunity> communityRepo)
        {
            _userCommunityRepo = userCommunityRepo;
            _communityRepo = communityRepo;
        }

        public IGenericRepository<UserCommunity> CommunityRepo { get; }

        public async Task<List<CommunityByUserDto>> Handle(GetAllCommunitiesByUserQuery request, CancellationToken cancellationToken)
        {
            var userCommunities = _userCommunityRepo.Query()
                .Where(uc => uc.UserId == request.UserId)
                .Include(uc => uc.Community) 
                .ToList();

            var result = userCommunities
                .Select(uc => new CommunityByUserDto
                {
                    Id = uc.CommunityId,
                    Name = uc.Community.Name,
                    Description = uc.Community.Description,
                    Role = uc.Role
                })
                .OrderByDescending(c => c.Role) 
                .ToList();
            return result;
        }
    }
}