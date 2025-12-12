using CommunityNoticeBoard.Application.IRepository;
using CommunityNoticeBoard.Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityNoticeBoard.Application.Features.Community.Commands.DeleteCommunity
{
    public class DeleteCommunityCommandValidator : AbstractValidator<DeleteCommunityCommand>
    {
        private readonly IGenericRepository<CommunityNoticeBoard.Domain.Entities.UserCommunity> _userCommunityRepo;
        private readonly IGenericRepository<CommunityNoticeBoard.Domain.Entities.Community> _communityRepo;

        public DeleteCommunityCommandValidator(
            IGenericRepository<CommunityNoticeBoard.Domain.Entities.UserCommunity> userCommunityRepo,
            IGenericRepository<CommunityNoticeBoard.Domain.Entities.Community> communityRepo)
        {
            _userCommunityRepo = userCommunityRepo;
            _communityRepo = communityRepo;

            RuleFor(x => x.CommunityId)
                .GreaterThan(0)
                .MustAsync(CommunityExists).WithMessage("Community does not exist.");

            RuleFor(x => x.UserId)
                .GreaterThan(0)
                .MustAsync(IsAdmin).WithMessage("Only admin can delete this community.");
        }

        private async Task<bool> CommunityExists(int communityId, CancellationToken cancellationToken)
        {
            var community = await _communityRepo.GetByIdAsync(communityId);
            return community != null;
        }

        private async Task<bool> IsAdmin(DeleteCommunityCommand command, int userId, CancellationToken cancellationToken)
        {
            var userCommunity = (await _userCommunityRepo.GetAllAsync())
                .FirstOrDefault(uc => uc.CommunityId == command.CommunityId && uc.UserId == userId);

            return userCommunity != null && userCommunity.Role == CommunityRole.Admin;
        }
    
    }
}
