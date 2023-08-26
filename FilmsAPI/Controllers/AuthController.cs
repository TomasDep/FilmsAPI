using FilmsAPI.GeneralDto.Auth;
using FilmsAPI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FilmsAPI.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthServices _authServices;

        public AuthController(IAuthServices authServices)
        {
            _authServices = authServices;
        }

        [HttpPost("register")]
        public Task<ActionResult<AuthDto>> Register([FromBody] AuthRegisterDto authRegisterDto)
        {
            return _authServices.Register(authRegisterDto);
        }

        [HttpPost("login")]
        public Task<ActionResult<AuthDto>> Login([FromBody] AuthRegisterDto authRegisterDto)
        {
            return _authServices.Login(authRegisterDto);
        }

        [HttpGet("renew")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public Task<ActionResult<AuthDto>> Renew()
        {
            var emailClaim = HttpContext.User.Claims.Where(claim => claim.Type == "email").FirstOrDefault();
            return _authServices.Renew(emailClaim);
        }

        [HttpPost("grant/admin")]
        public Task<ActionResult> GrantAdmin([FromBody] UpdateAuthDto updateAuthDto)
        {
            return _authServices.GrantAdmin(updateAuthDto);
        }

        [HttpPost("remove/admin")]
        public Task<ActionResult> RemoveAdmin([FromBody] UpdateAuthDto updateAuthDto)
        {
            return _authServices.RemoveAdmin(updateAuthDto);
        }
    }
}