using CommunityNoticeBoard.Application.Features.Community.Commands.CreateCommunityCommand;
using CommunityNoticeBoard.Application.Features.Community.Commands.DeleteCommunity;
using CommunityNoticeBoard.Application.Features.Community.Commands.JoinCommunity;
using CommunityNoticeBoard.Application.Features.Community.Queries.GetAllCommunitiesByUser;
using CommunityNoticeBoard.Application.Features.Community.Queries.GetAllCommunitiesNotPartOf;
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
        // CREATE community
        [HttpPost]
        public async Task<IActionResult> CreateCommunity([FromBody] CreateCommunityCommand command)
        {
            try
            {
                var communityId = await _mediator.Send(command);

                return Ok("created successfully");
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

                return Ok("Deleted Successfully"); // 204
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
        // GET: api/community/not-part-of?userId=1
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

        // GET: api/community/user-communities?userId=1
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
        // POST: api/community/join
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
    }
}