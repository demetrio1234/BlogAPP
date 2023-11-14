using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebAPP.API.Models.DTO.RequestDTO;

namespace WebAPP.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;

        public AuthController(UserManager<IdentityUser> userManager)
        {
            this.userManager = userManager;
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
    }
}
