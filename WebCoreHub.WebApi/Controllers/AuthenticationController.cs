using BCrypt.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebCoreHub.Dal;
using WebCoreHub.Models;
using WebCoreHub.WebApi.Jwt;

namespace WebCoreHub.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationRepository _authenticationRepository;

        private readonly ITokenManager _tokenManager;

        public AuthenticationController(IAuthenticationRepository authenticationRepository, ITokenManager tokenManager)
        {
            _authenticationRepository = authenticationRepository;
            _tokenManager = tokenManager;
        }

        [HttpPost("RegisterUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Create(User user)
        {
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(user.Password);

            user.Password = passwordHash;

            var result = _authenticationRepository.RegisterUser(user);

            if (result > 0)
            {
                return Ok();
            }

            return BadRequest();
        }

        [HttpPost("CheckCredentials")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<AuthResponse> GetDetails(User user)
        {
            var authUser = _authenticationRepository.CheckCredentials(user);

            if (authUser == null)
            {
                return NotFound();
            }

            if (!BCrypt.Net.BCrypt.Verify(user.Password, authUser.Password))
            {
                return BadRequest("Invalid credentials!");
            }

            var roleName = _authenticationRepository.GetUserRole(authUser.RoleId);

            var authResponse = new AuthResponse()
            {
                IsAuthenticated = true,
                Role = "Dummy Role",
                Token = _tokenManager.GenerateToken(user, roleName)
            };

            return Ok(authResponse);
        }
    }
}
