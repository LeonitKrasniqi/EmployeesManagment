using Microsoft.AspNetCore.Identity;

namespace EmployeesManagment.API.Repositories
{
    public interface ITokenRepository
    {
        string CreateJWTToken(IdentityUser user, List<string> roles);
    }
}
