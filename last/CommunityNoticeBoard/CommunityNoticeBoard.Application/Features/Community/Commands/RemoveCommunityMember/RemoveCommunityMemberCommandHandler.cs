using CommunityNoticeBoard.Application.IRepository;
using CommunityNoticeBoard.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityNoticeBoard.Application.Features.Community.Commands.RemoveCommunityMember
{
    public class RemoveCommunityMemberCommandHandler
        : IRequestHandler<RemoveCommunityMemberCommand, bool>
    {
        private readonly IGenericRepository<UserCommunity> _userCommunityRepo;

        public RemoveCommunityMemberCommandHandler(
            IGenericRepository<UserCommunity> userCommunityRepo)
        {
            _userCommunityRepo = userCommunityRepo;
        }

        public async Task<bool> Handle(
            RemoveCommunityMemberCommand request,
            CancellationToken cancellationToken)
        {
            var member = (await _userCommunityRepo.FindAsync(
                uc => uc.UserId == request.TargetUserId
                   && uc.CommunityId == request.CommunityId,
                cancellationToken))
                .FirstOrDefault();

            if (member == null)
                throw new Exception("Member not found.");

            await _userCommunityRepo.DeleteAsync(member);
            await _userCommunityRepo.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
