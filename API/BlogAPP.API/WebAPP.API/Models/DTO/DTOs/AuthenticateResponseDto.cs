namespace WebAPP.API.Models.DTO.DTOs
{
    public class AuthenticateResponseDto
    {
        public string Token { get; set; } = string.Empty;
        //public string RefreshToken { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public bool IsAuthenticated { get; set; } = false;
        public string ClientUri { get; set; } = string.Empty;

        public AuthenticateResponseDto() { }

        public AuthenticateResponseDto(string email, string token, bool isAuthenticated, string clientUri)
        {
            Email = email;
            Token = token;
            IsAuthenticated = isAuthenticated;
            ClientUri = clientUri;
        }
    }
}
