using WebAPP.API.Models.Domain;

namespace WebAPP.API.Models.DTO
{
    public class UserDto : BaseUser, IDomainMarker
    {
        public UserDto() { }

        public UserDto(User user) {
            Id = user.Id;
            Name = user.Name;
            Email = user.Email;
            HashedPassword = user.HashedPassword;
            Address = user.Address;
            City = user.City;
            Region = user.Region;
            PostalCode = user.PostalCode;
            Country = user.Country;
            Phone = user.Phone;
        }

        public Guid Id { get; set; }

    }
}
