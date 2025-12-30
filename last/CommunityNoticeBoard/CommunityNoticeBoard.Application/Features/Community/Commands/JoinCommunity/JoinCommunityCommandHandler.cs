using CommunityNoticeBoard.Application.IRepository;
using CommunityNoticeBoard.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityNoticeBoard.Application.Features.Community.Commands.JoinCommunity
{
    public class JoinCommunityCommandHandler : IRequestHandler<JoinCommunityCommand, bool>
    {
        private readonly IGenericRepository<UserCommunity> _userCommunityRepo;

        public JoinCommunityCommandHandler(IGenericRepository<UserCommunity> userCommunityRepo)
        {
            _userCommunityRepo = userCommunityRepo;
        }

        public async Task<bool> Handle(JoinCommunityCommand request, CancellationToken cancellationToken)
        {
            var userCommunity = new UserCommunity(request.UserId,request.CommunityId,CommunityRole.Member);

            await _userCommunityRepo.AddAsync(userCommunity, cancellationToken);
            await _userCommunityRepo.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
