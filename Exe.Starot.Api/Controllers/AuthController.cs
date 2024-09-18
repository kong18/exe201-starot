using Exe.Starot.Application.Interfaces;
using Exe.Starot.Domain.Entities.Base;
using Exe.Starot.Domain.Entities.Repositories;
using IdentityModel;
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

        public AuthController(IUserRepository userRepository, IJwtService jwtService)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
        }

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
    }
}
