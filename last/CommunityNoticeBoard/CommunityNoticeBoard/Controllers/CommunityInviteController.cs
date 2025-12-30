using CommunityNoticeBoard.Application.Features.CommunityInvite.command.AcceptCommunityInvite;
using CommunityNoticeBoard.Application.Features.CommunityInvite.command.CreateCommunityInvite;
using CommunityNoticeBoard.Application.Features.CommunityInvite.command.InviteUserToCommunity;
using CommunityNoticeBoard.Application.Features.CommunityInvite.command.RejectCommunityInvite;
using CommunityNoticeBoard.Application.Features.CommunityInvite.Query.GetAvailableUsersForCommunity;
using CommunityNoticeBoard.Application.Features.CommunityInvite.Query.GetInvitesByUserId;
using CommunityNoticeBoard.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CommunityNoticeBoard.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommunityInviteController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CommunityInviteController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("{communityId}/invite")]
        public async Task<IActionResult> InviteUser(int communityId, [FromQuery] string invitedEmail, [FromQuery] int? invitedUserId, [FromQuery] int invitedByUserId)
        {
            await _mediator.Send(new InviteUserToCommunityCommand
            {
                CommunityId = communityId,
                InvitedEmail = invitedEmail,
                InvitedUserId = invitedUserId,
                InvitedByUserId = invitedByUserId,
            });
            return Ok(new { message = "Invite sent successfully" });
        }

        [HttpPost("invite/{inviteId}/accept")]
        public async Task<IActionResult> AcceptInvite(
            int inviteId,
            [FromQuery] int userId)
        {
            await _mediator.Send(
                new AcceptCommunityInviteCommand
                {
                    InviteId = inviteId,
                    UserId = userId
                });
            return Ok(new { message = "Joined community successfully" });
        }
        [HttpGet("{communityId}/available-users")]
        public async Task<IActionResult> GetAvailableUsers(int communityId, [FromQuery] int adminUserId)
        {
            var result = await _mediator.Send(
                new GetAvailableUsersForCommunityQuery
                {
                    CommunityId = communityId,
                    AdminUserId = adminUserId
                });

            return Ok(result);
        }
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetInvitesForUser(int userId)
        {
            var invites = await _mediator.Send(
                new GetInvitesByUserIdQuery { UserId = userId });

            return Ok(invites);
        }
        [HttpPost("invite/{inviteId}/reject")]
        public async Task<IActionResult> RejectInvite(int inviteId, [FromQuery] int userId)
        {
            var result = await _mediator.Send(
                new RejectCommunityInviteCommand
                {
                    InviteId = inviteId,
                    UserId = userId
                });

            return Ok(new { message = result });
        }
        [HttpPost("{communityId}/invitebyemail")]
        public async Task<IActionResult> Invite(
            int communityId,
            [FromQuery] int invitedByUserId,
            [FromQuery] string invitedEmail)
        {
            var result = await _mediator.Send(new CreateCommunityInviteCommand
            {
                CommunityId = communityId,
                invitedByUserId = invitedByUserId,
                invitedEmail = invitedEmail
            });

            return Ok(new { message = result });
        }
    }
}
