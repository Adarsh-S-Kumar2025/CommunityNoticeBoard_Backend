using CommunityNoticeBoard.Application.Features.Community.Commands.CreateCommunityCommand;
using CommunityNoticeBoard.Application.Features.Community.Commands.DeleteCommunity;
using CommunityNoticeBoard.Application.Features.Community.Commands.JoinCommunity;
using CommunityNoticeBoard.Application.Features.Community.Commands.LeaveCommunity;
using CommunityNoticeBoard.Application.Features.Community.Commands.RemoveCommunityMember;
using CommunityNoticeBoard.Application.Features.Community.Queries.CommunityMember;
using CommunityNoticeBoard.Application.Features.Community.Queries.GetAllCommunitiesByUser;
using CommunityNoticeBoard.Application.Features.Community.Queries.GetAllCommunitiesNotPartOf;
using CommunityNoticeBoard.Application.Features.Community.Queries.GetCommunityById;
using CommunityNoticeBoard.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace CommunityNoticeBoard.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommunityController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CommunityController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        public async Task<IActionResult> CreateCommunity([FromBody] CreateCommunityCommand command)
        {
            try
            {
                var communityId = await _mediator.Send(command);

                return Ok(new { Error = "community created successfully" });
            }
            catch (FluentValidation.ValidationException ex)
            {
                return BadRequest(new { Errors = ex.Errors.Select(e => e.ErrorMessage) });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCommunity(int id, [FromQuery] int userId)
        {
            try
            {
                var command = new DeleteCommunityCommand
                {
                    CommunityId = id,
                    UserId = userId
                };

                var result = await _mediator.Send(command);
                if (!result)
                    return BadRequest(new { Error = "Failed to delete community." });

                return Ok(new { message = "community deleted successfully" }); // 204
            }
            catch (FluentValidation.ValidationException ex)
            {
                return BadRequest(new { Errors = ex.Errors.Select(e => e.ErrorMessage) });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }
        [HttpGet("not-part-of")]
        public async Task<IActionResult> GetAllCommunitiesNotPartOf([FromQuery] int userId)
        {
            var query = new GetAllCommunitiesNotPartOfQuery
            {
                UserId = userId
            };

            var communities = await _mediator.Send(query);
            return Ok(communities);
        }

        [HttpGet("user-communities")]
        public async Task<IActionResult> GetAllCommunitiesByUser([FromQuery] int userId)
        {
            var query = new GetAllCommunitiesByUserQuery
            {
                UserId = userId
            };

            var communities = await _mediator.Send(query);
            return Ok(communities);
        }
        [HttpPost("join")]
        public async Task<IActionResult> JoinCommunity([FromBody] JoinCommunityCommand command)
        {
            try
            {
                var result = await _mediator.Send(command);
                if (!result)
                    return BadRequest(new { Error = "Failed to join community." });

                return Ok(new { Message = "Successfully joined the community." });
            }
            catch (FluentValidation.ValidationException ex)
            {
                return BadRequest(new { Errors = ex.Errors.Select(e => e.ErrorMessage) });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }
        [HttpDelete("leave")]
        public async Task<IActionResult> LeaveCommunity([FromQuery] int userId,[FromQuery] int communityId)
        {
            try
            {
                var result = await _mediator.Send(new LeaveCommunityCommand
                {
                    UserId = userId,
                    CommunityId = communityId
                });

                return Ok(new { Message = "leave community success" });
            }
            catch (FluentValidation.ValidationException ex)
            {
                return BadRequest(ex.Errors.Select(e => e.ErrorMessage));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{communityId}")]
        public async Task<IActionResult> GetCommunityById(int communityId,[FromQuery] int? userId)
        {
            var result = await _mediator.Send(
                new GetCommunityByIdQuery
                {
                    CommunityId = communityId,
                    UserId = userId
                });

            return Ok(result);
        }
        [HttpGet("{communityId}/members")]
        public async Task<IActionResult> GetCommunityMembers(
            int communityId,
            [FromQuery] int userId)
        {
            var result = await _mediator.Send(new GetCommunityMembersQuery
            {
                CommunityId = communityId,
                UserId = userId
            });

            return Ok(result);
        }
        [HttpDelete("{communityId}/members/{targetUserId}")]
        public async Task<IActionResult> RemoveMember(
            int communityId,
            int targetUserId,
            [FromQuery] int adminUserId)
        {
            try
            {
                var result = await _mediator.Send(new RemoveCommunityMemberCommand
                {
                    CommunityId = communityId,
                    AdminUserId = adminUserId,
                    TargetUserId = targetUserId
                });

                return Ok(new { Removed = result });
            }
            catch (FluentValidation.ValidationException ex)
            {
                return BadRequest(ex.Errors.Select(e => e.ErrorMessage));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
    