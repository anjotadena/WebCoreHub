using BCrypt.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebCoreHub.Dal;
using WebCoreHub.Models;

namespace WebCoreHub.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationRepository _authenticationRepository;

        public AuthenticationController(IAuthenticationRepository authenticationRepository)
        {
            _authenticationRepository = authenticationRepository;
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

            var authResponse = new AuthResponse() { IsAuthenticated = true, Role = "Dummy Role", Token = "Dummy Token" };
            
            return Ok(authResponse);
        }
    }
}
