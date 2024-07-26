namespace WebAPP.API.Models.DTO.RequestDTO
{
    public class AuthenticateRequestDto
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        public AuthenticateRequestDto() { }

        public AuthenticateRequestDto(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }
}
