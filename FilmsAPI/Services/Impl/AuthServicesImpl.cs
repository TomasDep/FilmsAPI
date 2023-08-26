using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FilmsAPI.GeneralDto.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace FilmsAPI.Services
{
    public class AuthServicesImpl : IAuthServices
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly SignInManager<IdentityUser> _signingManager;
        private readonly ILogger<AuthServicesImpl> _logger;

        public AuthServicesImpl(
            UserManager<IdentityUser> userManager,
            IConfiguration configuration,
            SignInManager<IdentityUser> signingManager,
            ILogger<AuthServicesImpl> logger
        )
        {
            _userManager = userManager;
            _configuration = configuration;
            _signingManager = signingManager;
            _logger = logger;
        }

        public async Task<ActionResult> GrantAdmin(UpdateAuthDto updateAuthDto)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(updateAuthDto.Email);
                await _userManager.AddClaimAsync(user, new Claim("Admin", "1"));
                return new NoContentResult();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex}");
                return new ObjectResult($"Internal Services Exception: {ex.Message}") { StatusCode = 500 };
            }
        }

        public async Task<ActionResult<AuthDto>> Login([FromBody] AuthRegisterDto authRegisterDto)
        {
            try
            {
                var result = await _signingManager.PasswordSignInAsync(
                    authRegisterDto.Email,
                    authRegisterDto.Password,
                    isPersistent: false,
                    lockoutOnFailure: false
                );
                if (result.Succeeded)
                {
                    return await _jwtTokenBuilder(authRegisterDto);
                }
                else
                {
                    return new BadRequestObjectResult("Login failed");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex}");
                return new ObjectResult($"Internal Services Exception: {ex.Message}") { StatusCode = 500 };
            }
        }

        public async Task<ActionResult<AuthDto>> Register([FromBody] AuthRegisterDto authRegisterDto)
        {
            try
            {
                var user = new IdentityUser { UserName = authRegisterDto.Email, Email = authRegisterDto.Email };
                var result = await _userManager.CreateAsync(user, authRegisterDto.Password);
                if (result.Succeeded)
                {
                    return await _jwtTokenBuilder(authRegisterDto);
                }
                else
                {
                    return new BadRequestObjectResult(result.Errors);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex}");
                return new ObjectResult($"Internal Services Exception: {ex.Message}") { StatusCode = 500 };
            }
        }

        public async Task<ActionResult> RemoveAdmin(UpdateAuthDto updateAuthDto)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(updateAuthDto.Email);
                await _userManager.RemoveClaimAsync(user, new Claim("Admin", "1"));
                return new NoContentResult();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex}");
                return new ObjectResult($"Internal Services Exception: {ex.Message}") { StatusCode = 500 };
            }
        }

        public async Task<ActionResult<AuthDto>> Renew(Claim emailClaim)
        {
            try
            {
                var email = emailClaim.Value;
                var userCredentials = new AuthRegisterDto() { Email = email };
                return await _jwtTokenBuilder(userCredentials);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex}");
                return new ObjectResult($"Internal Services Exception: {ex.Message}") { StatusCode = 500 };
            }
        }

        private async Task<AuthDto> _jwtTokenBuilder(AuthRegisterDto authRegisterDto)
        {
            var claims = new List<Claim>() {
                new Claim("email", authRegisterDto.Email),
            };
            var user = await _userManager.FindByEmailAsync(authRegisterDto.Email);
            var claimsDB = await _userManager.GetClaimsAsync(user);
            claims.AddRange(claimsDB);
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["keyJwt"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiration = DateTime.UtcNow.AddMinutes(60);
            var securityToken = new JwtSecurityToken(issuer: null, audience: null, claims: claims, expires: expiration, signingCredentials: creds);
            return new AuthDto() { Token = new JwtSecurityTokenHandler().WriteToken(securityToken), Expiration = expiration };
        }
    }
}