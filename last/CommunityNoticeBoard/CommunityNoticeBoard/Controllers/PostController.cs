using CommunityNoticeBoard.Application.Features.Post.Commands.CreatePost;
using CommunityNoticeBoard.Application.Features.Post.Commands.DeletePost;
using CommunityNoticeBoard.Application.Features.Post.Commands.MarkAsResolved;
using CommunityNoticeBoard.Application.Features.Post.Commands.PinPost;
using CommunityNoticeBoard.Application.Features.Post.Commands.PublishPost;
using CommunityNoticeBoard.Application.Features.Post.Commands.UpdateDraftPost;
using CommunityNoticeBoard.Application.Features.Post.Queries.GetDraftPosts;
using CommunityNoticeBoard.Application.Features.Post.Queries.GetExpiredPostsByCommunityAdmin;
using CommunityNoticeBoard.Application.Features.Post.Queries.GetPostsByCommunityId;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
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

        [HttpPost]
        public async Task<IActionResult> CreatePost([FromBody] CreatePostCommand command)
        {
            try
            {
                var postId = await _mediator.Send(command);
                return Ok(new { message = "post created successfully" });
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
        [HttpGet("community/{communityId}")]
        public async Task<IActionResult> GetPostsByCommunityId(int communityId, [FromQuery] int UserId, [FromQuery] string? Title, [FromQuery] string? Category)
        {
            var posts = await _mediator.Send(new GetPostsByCommunityIdQuery
            {
                CommunityId = communityId,
                UserId = UserId,
                Title = Title,
                Category = Category
            });

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
        [HttpPut("{postId}/resolve")]
        public async Task<IActionResult> ResolvePost(int postId, [FromQuery] int userId)
        {
            try
            {
                var success = await _mediator.Send(
                    new MarkAsResolvedCommand
                    {
                        PostId = postId,
                        UserId = userId
                    });

                return Ok(new
                {
                    message = "Post marked as resolved"
                });
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
        [HttpPut("{postId}/pin")]
        public async Task<IActionResult> PinPost(int postId, [FromQuery] int userId, [FromQuery] bool pin)
        {
            try
            {
                var result = await _mediator.Send(new PinPostCommand { PostId = postId, UserId = userId, Pin = pin });

                if (!result)
                    return NotFound("Post not found");

                return Ok(new
                {
                    message = pin ? "Post pinned" : "Post unpinned"
                });
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
        [HttpGet("community/{communityId}/drafts")]
        public async Task<IActionResult> GetDraftPosts(int communityId, [FromQuery] int userId)
        {
            var draft = await _mediator.Send(new GetDraftPostsQuery
            {
                CommunityId = communityId,
                UserId = userId
            });

            return Ok(draft);
        }
        [HttpPut("{postId}/publish")]
        public async Task<IActionResult> PublishPost(int postId, [FromQuery] int userId)
        {
            await _mediator.Send(new PublishPostCommand { PostId = postId, UserId = userId });
            return Ok(new { message = "Post published successfully" });
        }

        [HttpPut("{postId}/draft")]
        public async Task<IActionResult> UpdateDraft(int postId,[FromQuery] int userId,[FromBody] UpdateDraftPostCommand command)
        {
            command.PostId = postId;
            command.UserId = userId;

            await _mediator.Send(command);
            return Ok(new { message = "Draft updated" });
        }


    }
}
