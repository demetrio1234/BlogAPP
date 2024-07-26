using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.IdentityModel.Tokens;
using System.Web;
using WebAPP.API.Models.Domain;
using WebAPP.API.Models.DTO.DTOs;
using WebAPP.API.Models.DTO.RequestDTO;
using WebAPP.API.Models.ServiceModels;
using WebAPP.API.Repositories.Interface;
using WebAPP.API.Services.Interfaces;

namespace WebAPP.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ITokenRepository _tokenRepository;
        private readonly IUserRepository _userRepository;
        private readonly IEmailService _emailSenderService;
        public AuthController(
            UserManager<IdentityUser> userManager,
            ITokenRepository tokenRepository,
            IUserRepository userRepository,
            IEmailService emailService)
        {
            _userManager = userManager;
            _tokenRepository = tokenRepository;
            _userRepository = userRepository;
            _emailSenderService = emailService;
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

            IdentityResult identityResult = await _userManager.CreateAsync(user, request.Password);

            if (identityResult.Succeeded)
            {
                identityResult = await _userManager.AddToRoleAsync(user, "Reader");//TODO: remove the roles arrray from the RegisterRequestDto since it'll be determinate from the role added here

                string? token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

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
                    User? newUser = await _userRepository.CreateAsync(u); //TODO: simplify by removing the User u and adding the properties directly to the CreateAsync method

                    if (newUser is null)
                        return StatusCode(StatusCodes.Status500InternalServerError);

                    UserDto userResponseDto = new()
                    {
                        Email = newUser.Email,
                        Name = newUser.Name,
                        Address = newUser.Address,
                        City = newUser.City,
                        PostalCode = newUser.PostalCode,
                        Country = newUser.Country,
                        Region = newUser.Region,
                        Phone = user.PhoneNumber,
                    };

                    var callbackUrl = QueryHelpers.AddQueryString(request.ClientUri, "token", token);

                    Message message = new(
                        [request.Email], 
                        "Confirm your email",
                        $"<h1>Confirm your email</h1><p>Please confirm your email by clicking <a href='{callbackUrl}'>here</a></p>");

                    _emailSenderService.SendEmail(message);

                    return Ok((userResponseDto, token));
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

        [HttpPost]
        public async Task<IActionResult> Authenticate([FromBody] AuthenticateRequestDto request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null || string.IsNullOrWhiteSpace(user.Email))
                return BadRequest("Invalid request");

            if (!_userManager.IsEmailConfirmedAsync(user).Result)
                return BadRequest("Email not confirmed");

            if (!await _userManager.CheckPasswordAsync(user, request.Password))
                return BadRequest("Invalid credentials");

            var roles = await _userManager.GetRolesAsync(user);
            var token = _tokenRepository.CreateJwtToken(user, roles);

            string clientUri = "";

            return Ok(new AuthenticateResponseDto(user.Email, token, true, clientUri));
        }

        [HttpGet]
        [Route("emailconfirmation")]
        public async Task<IActionResult> ConfirmEmail([FromQuery] string email, [FromQuery] string token)
        {
            string decodedUrl = HttpUtility.UrlDecode(token);

            IdentityUser? user = await _userManager.FindByEmailAsync(email);

            if (user is null)
                return BadRequest("Invalid request");

            IdentityResult result = await _userManager.ConfirmEmailAsync(user, decodedUrl);

            if (result.Succeeded)
                return Ok("Email confirmed");

            return BadRequest("Invalid request");
        }
    }
}
