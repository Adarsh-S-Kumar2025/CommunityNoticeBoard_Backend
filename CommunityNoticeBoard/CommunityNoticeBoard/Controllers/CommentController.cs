using CommunityNoticeBoard.Application.Features.Comment.Commands.CreateComment;
using CommunityNoticeBoard.Application.Features.Comment.Queries.GetCommentByPost;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CommunityNoticeBoard.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {

        private readonly IMediator _mediator;

    public CommentController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateComment([FromBody] CreateCommentCommand command)
    {
        try
        {
            var commentId = await _mediator.Send(command);

            return Ok(new { message = "Comment created successfully" });
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

        [HttpGet("{postId}")]
        public async Task<IActionResult> GetCommentsByPostId(int postId)
        {
            var comments = await _mediator.Send(
                new GetCommentsByPostIdQuery { PostId = postId });

            return Ok(comments);
        }
    }
}