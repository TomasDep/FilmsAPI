using System.Security.Claims;
using FilmsAPI.GeneralDto.Auth;
using Microsoft.AspNetCore.Mvc;

namespace FilmsAPI.Services
{
    public interface IAuthServices
    {
        Task<ActionResult<AuthDto>> Register(AuthRegisterDto authRegisterDto);
        Task<ActionResult<AuthDto>> Login(AuthRegisterDto authRegisterDto);
        Task<ActionResult<AuthDto>> Renew(Claim emailClaim);
        Task<ActionResult> GrantAdmin(UpdateAuthDto updateAuthDto);
        Task<ActionResult> RemoveAdmin(UpdateAuthDto updateAuthDto);
    }
}