using CommunityNoticeBoard.Application.IRepository;
using CommunityNoticeBoard.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityNoticeBoard.Application.Features.Community.Commands.LeaveCommunity
{
    public class LeaveCommunityCommandHandler
        : IRequestHandler<LeaveCommunityCommand, bool>
    {
        private readonly IGenericRepository<UserCommunity> _userCommunityRepo;

        public LeaveCommunityCommandHandler(
            IGenericRepository<UserCommunity> userCommunityRepo)
        {
            _userCommunityRepo = userCommunityRepo;
        }

        public async Task<bool> Handle(
            LeaveCommunityCommand request,
            CancellationToken cancellationToken)
        {
            var userCommunity = (await _userCommunityRepo.FindAsync(
                uc => uc.UserId == request.UserId
                   && uc.CommunityId == request.CommunityId,
                cancellationToken))
                .FirstOrDefault();

            if (userCommunity == null)
                throw new Exception("User is not a member of this community.");

            await _userCommunityRepo.DeleteAsync(userCommunity);
            await _userCommunityRepo.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}