using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPP.API.Data;
using WebAPP.API.Models.Doamin;
using WebAPP.API.Models.DTO.DTOs;
using WebAPP.API.Models.DTO.RequestDTO;

namespace WebAPP.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly ApplicationDbContext dbContext;

        public UserController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserRequestDto request)
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

            await dbContext.AddAsync(user);
            await dbContext.SaveChangesAsync();

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
    }
}
