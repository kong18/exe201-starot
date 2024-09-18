using Exe.Starot.Application.Common.Interfaces;
using Exe.Starot.Application.User.Authenticate;
using Exe.Starot.Domain.Entities.Base;
using Exe.Starot.Domain.Entities.Repositories;
using IdentityModel;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Exe.Starot.Api.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtService _jwtService;
        private readonly IMediator _mediator;
        public AuthController(IUserRepository userRepository, IJwtService jwtService, IMediator mediator)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
            _mediator = mediator;
        }

        // Refresh token API to issue a new access token using the refresh token
        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] TokenRequest tokenRequest)
        {
            var principal = _jwtService.GetPrincipalFromExpiredToken(tokenRequest.Token);
            var userEmail = principal?.FindFirst(JwtClaimTypes.Email)?.Value;

            if (userEmail == null) return Unauthorized();

            var user = await _userRepository.FindAsync(u => u.Email == userEmail);
            if (user == null || !user.IsRefreshTokenValid(tokenRequest.RefreshToken))
            {
                return Unauthorized("Invalid refresh token");
            }

            var newJwtToken = _jwtService.CreateToken(user.Id, user.Email);
            var newRefreshToken = _jwtService.GenerateRefreshToken();

            await _userRepository.UpdateRefreshTokenAsync(user, newRefreshToken, DateTime.UtcNow.AddDays(30));

            return Ok(new
            {
                Token = newJwtToken,
                RefreshToken = newRefreshToken
            });
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginQuery loginQuery, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(loginQuery, cancellationToken);
            return Ok(result);
        }
    }

}
