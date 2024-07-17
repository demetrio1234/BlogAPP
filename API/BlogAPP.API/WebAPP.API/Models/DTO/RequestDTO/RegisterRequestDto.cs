using System.ComponentModel.DataAnnotations;

namespace WebAPP.API.Models.DTO.RequestDTO
{
    public class RegisterRequestDto
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = null!;
        
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        public string[] Roles { get; set; } = null!;
    }
}
