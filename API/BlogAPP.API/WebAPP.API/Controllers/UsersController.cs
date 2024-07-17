using Microsoft.AspNetCore.Authorization;
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

        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;

        public UsersController(IUserRepository userRepository, IMapper mapper)
        {
            this.userRepository = userRepository;
            this.mapper = mapper;
        }

        [HttpPost]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserRequestDto request)
        {
            User user = new()
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

        [HttpGet]
        //[Authorize(Roles = "Writer")]
        public async Task<IActionResult> GetAllAsync() //[FromBody] GetUsersRequestDto request
        {
            IEnumerable<User> users = await userRepository.GetAllAsync();

                var usersDtos = mapper.Map<List<UserDto>>(users);

                if (usersDtos == null)
            {
                    return NotFound();
                }

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
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] Guid Id)
        {

            User? user = await userRepository.GetByIdAsync(Id);

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

        [HttpPut]
        [Route("{Id:Guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> UpdateByIdAsync([FromRoute] Guid Id, UpdateUserRequestDto request)
        {
            User? user = new()
            {
                Id = Id,
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

            await userRepository.UpdateAsync(user);

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

        [HttpDelete]
        [Route("{Id:Guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> DeleteByIdAsync([FromRoute] Guid Id)
        {
            User? existingUser = await userRepository.DeleteAsync(Id);

            if (existingUser == null)
                return NotFound();

            UserDto response = new()
            {
                Id = existingUser.Id,
                Name = existingUser.Name,
                Email = existingUser.Email,
                HashedPassword = existingUser.HashedPassword,
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
