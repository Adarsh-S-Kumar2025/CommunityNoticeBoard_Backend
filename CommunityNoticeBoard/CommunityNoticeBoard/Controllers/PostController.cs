using CommunityNoticeBoard.Application.Features.Post.Commands.CreatePost;
using CommunityNoticeBoard.Application.Features.Post.Commands.DeletePost;
using CommunityNoticeBoard.Application.Features.Post.Queries.GetExpiredPostsByCommunityAdmin;
using CommunityNoticeBoard.Application.Features.Post.Queries.GetPostsByCommunityId;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CommunityNoticeBoard.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PostController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // POST: api/post
        [HttpPost]
        public async Task<IActionResult> CreatePost([FromBody] CreatePostCommand command)
        {
            try
            {
                var postId = await _mediator.Send(command);
                return Ok(new { message = "User created successfully" });
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
        // GET: api/post/community/{communityId}
        [HttpGet("community/{communityId}")]
        public async Task<IActionResult> GetPostsByCommunityId(int communityId)
        {
            var query = new GetPostsByCommunityIdQuery { CommunityId = communityId };
            var posts = await _mediator.Send(query);
            return Ok(posts);
        }
        [HttpGet("expired-posts")]
        public async Task<IActionResult> GetExpiredPostsByAdmin(
            int userId, int communityId)
        {
            var query = new GetExpiredPostsByCommunityAdminQuery
            {
                UserId = userId,
                CommunityId = communityId
            };

            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpDelete("{postId}")]
        public async Task<IActionResult> DeletePost(
            int postId,
            [FromQuery] int userId)
        {
            try
            {
                var result = await _mediator.Send(new DeletePostCommand
                {
                    PostId = postId,
                    UserId = userId
                });

                return Ok(new { Deleted = result });
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
