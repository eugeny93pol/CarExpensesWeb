using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Threading.Tasks;
using CE.DataAccess.Dtos;
using CE.DataAccess.Models;
using Microsoft.AspNetCore.Mvc;

namespace CE.Service.Interfaces
{
    public interface IUserService
    {
        Task<ActionResult<UserDto>> Create(ClaimsPrincipal claims, CreateUserDto item);

        Task<ActionResult<UserDto>> GetOne(ClaimsPrincipal claims, Guid id,
            params Expression<Func<User, object>>[] includeProperties);

        Task<ActionResult<IEnumerable<UserDto>>> GetAll(ClaimsPrincipal claims,
            Expression<Func<User, bool>> filter = null,
            Func<IQueryable<User>, IOrderedQueryable<User>> orderBy = null,
            params Expression<Func<User, object>>[] includeProperties);

        Task<ActionResult> Update(ClaimsPrincipal claims, UpdateUserDto userDto);

        Task<IActionResult> Delete(ClaimsPrincipal claims, Guid id);

        /// <summary>
        /// Returns the <see cref="User"/> instance if email and password are correct, otherwise null if they are not correct or email is not registered.
        /// </summary>
        /// <param name="email">User's e-mail address</param>
        /// <param name="password">User password</param>
        /// <returns>The <see cref="User"/> instance if email and password are correct, or null if they are not correct or email is not registered</returns>
        Task<UserDto> Authenticate(string email, string password);
    }
}
