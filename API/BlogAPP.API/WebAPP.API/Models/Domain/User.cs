using Azure.Core;
using WebAPP.API.Models.DTO;

namespace WebAPP.API.Models.Domain
{
    public class User : BaseUser, IDomainMarker
    {
        public User() { }

        public User(UserDto userDto)
        {
            Name = userDto.Name;
            Email = userDto.Email;
            HashedPassword = userDto.HashedPassword;
            Address = userDto.Address;
            City = userDto.City;
            Region = userDto.Region;
            PostalCode = userDto.PostalCode;
            Country = userDto.Country;
            Phone = userDto.Phone;
        }

        public User(Guid Id, UserDto userDto)
        {
            this.Id = Id;
            Name = userDto.Name;
            Email = userDto.Email;
            HashedPassword = userDto.HashedPassword;
            Address = userDto.Address;
            City = userDto.City;
            Region = userDto.Region;
            PostalCode = userDto.PostalCode;
            Country = userDto.Country;
            Phone = userDto.Phone;
        }
        public Guid Id { get; set; }

    }
}
