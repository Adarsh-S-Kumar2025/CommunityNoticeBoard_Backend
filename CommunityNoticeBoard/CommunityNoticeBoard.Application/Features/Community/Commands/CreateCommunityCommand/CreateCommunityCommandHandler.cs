using CommunityNoticeBoard.Application.IRepository;
using CommunityNoticeBoard.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityNoticeBoard.Application.Features.Community.Commands.CreateCommunityCommand
{
    public class CreateCommunityCommandHandler:IRequestHandler<CreateCommunityCommand, int>
    {
        private readonly IGenericRepository<CommunityNoticeBoard.Domain.Entities.Community> _communityRepo;
        private readonly IGenericRepository<CommunityNoticeBoard.Domain.Entities.UserCommunity> _userCommunityRepo;

        public CreateCommunityCommandHandler(IGenericRepository<CommunityNoticeBoard.Domain.Entities.Community> communityRepo, IGenericRepository<CommunityNoticeBoard.Domain.Entities.UserCommunity> userCommunityRepo)
        {
            _communityRepo = communityRepo;
            _userCommunityRepo = userCommunityRepo;
        }

        public async Task<int> Handle(CreateCommunityCommand request, CancellationToken cancellationToken)
        {
            // STEP 1: Create community
            var community = new CommunityNoticeBoard.Domain.Entities.Community(request.Name, request.Description);

            await _communityRepo.AddAsync(community);
            await _communityRepo.SaveChangesAsync();   // Required to generate community.Id

            // STEP 2: Insert into UserCommunity junction table
            var userCommunity = new CommunityNoticeBoard.Domain.Entities.UserCommunity(
                request.UserId,
                community.Id,
                CommunityRole.Admin
            );
            await _userCommunityRepo.AddAsync(userCommunity);
            await _userCommunityRepo.SaveChangesAsync();

            return community.Id;
        }
    }
}
