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

namespace CommunityNoticeBoard.Application.Features.Community.Queries.CommunityMember
{
    public class GetCommunityMembersQueryHandler
        : IRequestHandler<GetCommunityMembersQuery, List<CommunityMemberDto>>
    {
        private readonly IGenericRepository<UserCommunity> _userCommunityRepo;

        public GetCommunityMembersQueryHandler(
            IGenericRepository<UserCommunity> userCommunityRepo)
        {
            _userCommunityRepo = userCommunityRepo;
        }

        public async Task<List<CommunityMemberDto>> Handle(
    GetCommunityMembersQuery request,
    CancellationToken cancellationToken)
        {
            var isAdmin = (await _userCommunityRepo.FindAsync(
                uc => uc.UserId == request.UserId
                   && uc.CommunityId == request.CommunityId,
                cancellationToken))
                .Any(uc => uc.Role == CommunityRole.Admin);

            if (!isAdmin)
                throw new Exception("Only community admin can view members.");

            var members = await _userCommunityRepo
                .Query()
                .Where(uc => uc.CommunityId == request.CommunityId)
                .Include(uc => uc.User)
                .OrderByDescending(uc => uc.Role)   
                .ThenBy(uc => uc.User.Name)
                .Select(uc => new CommunityMemberDto
                {
                    UserId = uc.UserId,
                    UserName = uc.User.Name,
                    UserRole = uc.Role.ToString() 
                })
                .ToListAsync(cancellationToken);

            return members;
        }
    }
}