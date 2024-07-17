using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using WebAPP.API.Models.DTO.DTOs;
using WebAPP.API.Models.DTO.RequestDTO;
using WebAPP.API.Repositories.Interface;

namespace WebAPP.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly ITokenRepository tokenRepository;
        public AuthController(UserManager<IdentityUser> userManager, ITokenRepository tokenRepository)
        {
            this.userManager = userManager;
            this.tokenRepository = tokenRepository;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto request)
        {

            //Create an IdentityUser instance
            IdentityUser user = new()
            {
                UserName = request.Email.Trim(),
                Email = request.Email.Trim(),
            };

            //Map it's properties to a dto

            IdentityResult identityResult = await userManager.CreateAsync(user, request.Password);

            if (identityResult.Succeeded)
            {
                identityResult = await userManager.AddToRoleAsync(user, "Reader");

                if (identityResult.Succeeded)
                    return Ok();


                if (identityResult.Errors.Any())
                {
                    foreach (IdentityError error in identityResult.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            else
            {
                if (identityResult.Errors.Any())
                {
                    foreach (IdentityError error in identityResult.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return ValidationProblem(ModelState);
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
        {
            IdentityUser? user = await userManager.FindByEmailAsync(request.Email);

            if (user is not null && !string.IsNullOrEmpty( user.Email ))
            {
                bool checkPW = await userManager.CheckPasswordAsync(user, request.Password);

                if (checkPW)
                {
                    var roles = await userManager.GetRolesAsync(user);

                    LoginResponseDto response = new ()
                    {
                        Email = user.Email,
                        Token = tokenRepository.CreateJwtToken(user, roles),
                        Roles = roles.ToList(),
                    };

                    return Ok(response);
                }
            }
            return Unauthorized();
        }

    }
}
