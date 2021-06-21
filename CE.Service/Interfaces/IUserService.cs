using System.Security.Claims;
using System.Threading.Tasks;
using CE.DataAccess;
using Microsoft.AspNetCore.Mvc;

namespace CE.Service.Interfaces
{
    public interface IUserService : IService<User>
    {
        /// <summary>
        /// Returns the <see cref="User"/> instance if email and password are correct, otherwise null if they are not correct or email is not registered.
        /// </summary>
        /// <param name="email">User's e-mail address</param>
        /// <param name="password">User password</param>
        /// <returns>The <see cref="User"/> instance if email and password are correct, or null if they are not correct or email is not registered</returns>
        Task<User> Authenticate(string email, string password);

        Task<ActionResult<User>> UpdatePartial(ClaimsPrincipal claims, User item);
    }
}
