using Microsoft.AspNetCore.Identity;

namespace WebAPP.API.Models.Domain
{
    public class User
    {
        public User(Guid id) { Id = id; }

        public Guid Id { get; private set; }
        public string Email { get; set; } = string.Empty; 
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Region { get; set; } = string.Empty;
        public string PostalCode { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
    }
}
