using Microsoft.AspNetCore.Mvc;
using WebAPP.API.Models.Domain;
using WebAPP.API.Models.DTO;
using WebAPP.API.Repositories.Implementation;
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
        public async Task<IActionResult> CreateUser([FromBody] UserDto request)
        {
            User user = new(request);

            user = await userRepository.CreateAsync(user);

            UserDto response = new(user);

            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            IEnumerable<User> users = await userRepository.GetAllUsersAsync();

            List<UserDto> response = new List<UserDto>();

            foreach (User user in users)
            {
                response.Add(new UserDto(user));
            }

            return Ok(response);
        }

        [HttpGet]
        [Route("{Id:Guid}")]
        public async Task<IActionResult> GetUserById([FromRoute] Guid Id)
        {

            User? user = await userRepository.GetUserByIdAsync(Id);

            if (user == null)
                return NotFound();

            UserDto response = new(user);

            return Ok(response);
        }

        [HttpPut]
        [Route("{Id:Guid}")]
        public async Task<IActionResult> UpdateUser([FromRoute] Guid Id, UserDto request)
        {
            User user = new(Id, request);

            User? updatedUser = await userRepository.UpdateUserAsync(user);

            if (updatedUser == null)
                return NotFound();

            UserDto response = new(updatedUser);

            return Ok(response);

        }

        [HttpDelete]
        [Route("{Id:Guid}")]
        public async Task<IActionResult> DeleteUser([FromRoute] Guid Id)
        {
            User? user = await userRepository.DeleteUserAsync(Id);

            if (user == null) return NotFound();

            UserDto response = new(user);

            return Ok(response);

        }

    }
}
