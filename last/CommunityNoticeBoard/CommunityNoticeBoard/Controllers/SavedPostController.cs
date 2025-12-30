using CommunityNoticeBoard.Application.Features.SavedPost.Command.SavePost;
using CommunityNoticeBoard.Application.Features.SavedPost.Command.UnsavePost;
using CommunityNoticeBoard.Application.Features.SavedPost.Queries.GetSavedPostsByUser;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CommunityNoticeBoard.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SavedPostController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SavedPostController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("posts/{postId}/save")]
        public async Task<IActionResult> SavePost(int postId, [FromQuery] int userId)
        {
            try
            { 
            var id = await _mediator.Send(new SavePostCommand{UserId = userId,PostId = postId});
            return Ok(new {Message = "Post saved successfully",SavedPostId = id});
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

        [HttpDelete("posts/{postId}/unsave")]
        public async Task<IActionResult> UnsavePost(int postId,[FromQuery] int userId)
        {
            await _mediator.Send(new UnsavePostCommand {UserId = userId,PostId = postId});

            return Ok(new { Message = "Post unsaved successfully" });
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetSavedPostsByUser(int userId, [FromQuery] string? title, [FromQuery] string? category, [FromQuery] string? communityName)
        {
            var posts = await _mediator.Send(
                new GetSavedPostsByUserQuery { UserId = userId,Title = title,
                    Category = category,
                    CommunityName = communityName
                });

            return Ok(posts);
        }
    }
}