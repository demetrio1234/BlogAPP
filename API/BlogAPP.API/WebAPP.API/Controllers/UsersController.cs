using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebAPP.API.Models.Domain;
using WebAPP.API.Models.DTO.DTOs;
using WebAPP.API.Models.DTO.RequestDTO;
using WebAPP.API.Repositories.Implementation;
using WebAPP.API.Repositories.Interface;

namespace WebAPP.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly Microsoft.AspNetCore.Identity.UserManager<IdentityUser> _userManager;

        public UsersController(IUserRepository userRepository, IMapper mapper, Microsoft.AspNetCore.Identity.UserManager<IdentityUser> userManager)
        {
            this._userRepository = userRepository;
            this._mapper = mapper;
            this._userManager = userManager;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllAsync() //[FromBody] GetUsersRequestDto request
        {
            try
            {
                var users = await _userRepository.GetAllAsync();

                var usersDtos = _mapper.Map<List<UserDto>>(users);

                if (usersDtos == null)
                    return NotFound();

                return Ok(usersDtos);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            finally
            {
                Console.WriteLine("Finally block");
            }
        }

        [HttpGet]
        [Route("{Id:Guid}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] Guid Id)
        {

            User? user = await _userRepository.GetByIdAsync(Id);

            if (user == null)
                return NotFound();

            if (string.IsNullOrEmpty(user.Email))
                return NotFound();

            UserDto response = new()
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Address = user.Address,
                City = user.City,
                Region = user.Region,
                PostalCode = user.PostalCode,
                Country = user.Country,
                Phone = user.Phone,
            };

            return Ok(response);
        }

        [HttpPut]
        [Route("{Id:Guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> UpdateByIdAsync([FromRoute] Guid Id, UpdateUserRequestDto request)
        {
            var identityUser = await _userManager.FindByIdAsync(Id.ToString());
            if (identityUser == null)
                return NotFound();

            identityUser.Email = request.Email;
            identityUser.UserName = request.Email;
            identityUser.NormalizedEmail = request.Email.ToUpper();
            identityUser.NormalizedUserName = request.Email.ToUpper();
            identityUser.PhoneNumber = request.Phone;

            var r = await _userManager.UpdateAsync(identityUser);
            if (!r.Succeeded)
                return BadRequest(r.Errors);

            User? user = new(Id)
            {
                Name = request.Name,
                Email = request.Email,
                Address = request.Address,
                City = request.City,
                Region = request.Region,
                PostalCode = request.PostalCode,
                Country = request.Country,
                Phone = request.Phone,
            };

            await _userRepository.UpdateAsync(user);

            UserDto response = new()
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Address = user.Address,
                City = user.City,
                Region = user.Region,
                PostalCode = user.PostalCode,
                Country = user.Country,
                Phone = user.Phone,
            };

            return Ok(response);
        }

        [HttpDelete]
        [Route("{Id:Guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> DeleteByIdAsync([FromRoute] Guid Id)
        {
            var identityUser = await _userManager.FindByIdAsync(Id.ToString());

            if(identityUser is null)
                return NotFound();

            var r = await _userManager.DeleteAsync(identityUser);

            if (!r.Succeeded)
                return BadRequest(r.Errors);

            User? existingUser = await _userRepository.DeleteAsync(Id);

            if (existingUser == null)
                return NotFound();

            if (string.IsNullOrEmpty(existingUser.Email))
                return NotFound();

            UserDto response = new()
            {
                Id = existingUser.Id,
                Name = existingUser.Name,
                Email = existingUser.Email,
                Address = existingUser.Address,
                City = existingUser.City,
                Region = existingUser.Region,
                PostalCode = existingUser.PostalCode,
                Country = existingUser.Country,
                Phone = existingUser.Phone,
            };

            return Ok(response);
        }
    }
}
