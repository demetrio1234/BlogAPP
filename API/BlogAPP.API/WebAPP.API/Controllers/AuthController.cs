using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using WebAPP.API.Models.Domain;
using WebAPP.API.Models.DTO.DTOs;
using WebAPP.API.Models.DTO.RequestDTO;
using WebAPP.API.Repositories.Interface;

namespace WebAPP.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ITokenRepository _tokenRepository;
        private readonly IUserRepository _userRepository;
        public AuthController(
            UserManager<IdentityUser> userManager,
            ITokenRepository tokenRepository,
            IUserRepository userRepository)
        {
            this._userManager = userManager;
            this._tokenRepository = tokenRepository;
            this._userRepository = userRepository;
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
                EmailConfirmed = false,
                PhoneNumber = request.PhoneNumber.Trim() ?? string.Empty,
                PhoneNumberConfirmed = false

            };

            //Map it's properties to a dto

            IdentityResult identityResult = await _userManager.CreateAsync(user, request.Password);

            if (identityResult.Succeeded)
            {
                identityResult = await _userManager.AddToRoleAsync(user, "Reader");

                var emailConfirmed = identityResult.Succeeded && await _userManager.IsEmailConfirmedAsync(user);

                if (emailConfirmed)
                    identityResult = await _userManager.AddToRoleAsync(user, "Writer");

                if (identityResult.Succeeded)
                {
                    User u = new(Guid.Parse(user.Id))
                    {
                        Name = request.Name.Trim(),
                        Email = request.Email.Trim(),
                        Address = request.Address.Trim(),
                        City = request.City.Trim(),
                        PostalCode = request.PostalCode.Trim(),
                        Country = request.Country.Trim(),
                        Region = request.Region.Trim()
                    };
                    await _userRepository.CreateAsync(u);
                    return Ok();
                }

                if (identityResult.Errors.Any())
                    foreach (IdentityError error in identityResult.Errors)
                        ModelState.AddModelError("", error.Description);
            }
            else
            {
                if (identityResult.Errors.Any())
                    foreach (IdentityError error in identityResult.Errors)
                        ModelState.AddModelError("", error.Description);
            }
            return ValidationProblem(ModelState);
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
        {
            IdentityUser? user = await _userManager.FindByEmailAsync(request.Email);

            if (user is not null && !string.IsNullOrEmpty(user.Email))
            {
                bool checkPW = await _userManager.CheckPasswordAsync(user, request.Password);

                if (checkPW)
                {
                    var roles = await _userManager.GetRolesAsync(user);

                    LoginResponseDto response = new()
                    {
                        Email = user.Email,
                        Token = _tokenRepository.CreateJwtToken(user, roles),
                        Roles = roles.ToList(),
                    };

                    return Ok(response);
                }
            }
            return Unauthorized();
        }

    }
}
