using Microsoft.AspNetCore.Identity;

namespace WebAPP.API.Repositories.Interface
{
    public interface ITokenRepository
    {
        string CreateJwtToken(IdentityUser user, IList<string> roles);
    }
}
