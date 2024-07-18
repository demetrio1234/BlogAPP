using System.ComponentModel.DataAnnotations;

namespace WebAPP.API.Models.DTO.RequestDTO
{
    public class LoginRequestDto
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = string.Empty;
        
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Password { get; set; } = string.Empty;

    }
}
