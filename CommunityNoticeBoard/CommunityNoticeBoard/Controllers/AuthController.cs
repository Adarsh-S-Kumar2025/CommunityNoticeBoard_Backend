using CommunityNoticeBoard.Application.Dtos;
using CommunityNoticeBoard.Application.Features.Auth.Login;
using CommunityNoticeBoard.Application.Features.Auth.Logout;
using CommunityNoticeBoard.Application.Features.Auth.RefreshToken;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CommunityNoticeBoard.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("login")]
        public async Task<ActionResult<LoginResultDto>> Login(LoginCommand request)
        {
            LoginResultDto result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpPost("refresh")]
        public async Task<ActionResult> RefreshToken(RefreshAccessTokenCommand request)
        {

            RefreshDTO result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpPost("logout")]
        public async Task<ActionResult<bool>> Logout(LogoutCommand request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }
    }
}
