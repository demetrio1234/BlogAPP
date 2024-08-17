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

        public string Name {  get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string PostalCode {  get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string Region { get; set; } = string.Empty;
        public string PhoneNumber { get; set;} = string.Empty;

        public string ClientUri { get; set; } = "https://localhost:7111/authentication/emailconfirmation";
    }
}
