using System.ComponentModel.DataAnnotations;

namespace WebAPP.API.Models.DTO.RequestDTO
{
    public class ForgottenPasswordRequestDto
    {
        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        public string? Client { get; set; }
    }
}
