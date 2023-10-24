using Microsoft.AspNetCore.Mvc;
using WebAPP.API.Models.Domain;
using WebAPP.API.Models.DTO.DTOs;
using WebAPP.API.Models.DTO.RequestDTO;
using WebAPP.API.Repositories.Interface;

namespace WebAPP.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        private readonly IUserRepository userRepository;

        public UsersController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserRequestDto request)
        {
            var user = new User()
            {
                Name = request.Name,
                Email = request.Email,
                HashedPassword = request.HashedPassword,
                Address = request.Address,
                City = request.City,
                Region = request.Region,
                PostalCode = request.PostalCode,
                Country = request.Country,
                Phone = request.Phone,
            };

            await userRepository.CreateAsync(user);

            var response = new UserDto()
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                HashedPassword = user.HashedPassword,
                Address = user.Address,
                City = user.City,
                Region = user.Region,
                PostalCode = user.PostalCode,
                Country = user.Country,
                Phone = user.Phone,
            };

            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            IEnumerable<User> users = await userRepository.GetAllUsersAsync();

            List<UserDto> response = new List<UserDto>();

            foreach (User user in users)
            {

                response.Add(new UserDto()
                {
                    Id = user.Id,
                    Name = user.Name,
                    Email = user.Email,
                    HashedPassword = user.HashedPassword,
                    Address = user.Address,
                    City = user.City,
                    Region = user.Region,
                    PostalCode = user.PostalCode,
                    Country = user.Country,
                    Phone = user.Phone,
                });

            }

            return Ok(response);
        }

        [HttpGet]
        [Route("{Id:Guid}")]
        public async Task<IActionResult> GetUserById([FromRoute] Guid Id)
        {

            User? user = await userRepository.GetUserByIdAsync(Id);

            if (user == null)
            {
                return NotFound();
            }

            UserDto response = new()
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                HashedPassword = user.HashedPassword,
                Address = user.Address,
                City = user.City,
                Region = user.Region,
                PostalCode = user.PostalCode,
                Country = user.Country,
                Phone = user.Phone,
            };

            return Ok(response);
        }

    }
}
