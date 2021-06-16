using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Threading.Tasks;
using CE.DataAccess;
using CE.DataAccess.Constants;
using CE.Repository;
using CE.Repository.Interfaces;
using CE.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace CE.Service.Implementations
{
    public class UserService : IUserService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly UserRepository _userRepository;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork as UnitOfWork;
            _userRepository = _unitOfWork?.UserRepository;
        }


        public async Task<ActionResult<User>> Create(ClaimsPrincipal claims, User item)
        {
            var roleUser = await _unitOfWork
                .RoleRepository
                .FirstOrDefault(r => r.Name == RolesConstants.User);

            var user = await CreateUser(item, roleUser);
            if (user == null)
                return new BadRequestObjectResult("That e-mail already registered.");
            
            //await _userSettingsService.Create(new UserSettings(user.Id));
            return new OkObjectResult(user);
        }

        public async Task<ActionResult<User>> GetOne(
            ClaimsPrincipal claims,
            Guid id,
            params Expression<Func<User, object>>[] includeProperties)
        {
            if (!IsHasAccess(claims, id))
                return new ForbidResult();
            var user = await _userRepository.GetById(id, includeProperties);
            return user != null ? new OkObjectResult(user) : new NotFoundObjectResult(id);
        }

        public async Task<ActionResult<IEnumerable<User>>> GetAll(
            ClaimsPrincipal claims = null, 
            Expression<Func<User, bool>> filter = null, 
            Func<IQueryable<User>, IOrderedQueryable<User>> orderBy = null, 
            params Expression<Func<User, object>>[] includeProperties)
        {
            var users = await _userRepository.GetAll(filter, orderBy, includeProperties);
            return new OkObjectResult(users.ToList());
        }

        public async Task<IActionResult> Delete(ClaimsPrincipal claims, Guid id)
        {
            if (!IsHasAccess(claims, id))
                return new ForbidResult();
            var user = await _userRepository.GetById(id);
            if (user == null)
                return new NotFoundObjectResult(id);
            await _userRepository.Remove(user);
            return new NoContentResult();
        }


        public async Task<User> Authenticate(string email, string password)
        {
            var user = await _userRepository.FirstOrDefault(u => u.Email == email);
            if (user != null && BCrypt.Net.BCrypt.Verify(password, user.Password))
                return user;
            return null;
        }

        public async Task UpdatePartial(User savedUser, User user)
        {
            savedUser.Name = user.Name ?? savedUser.Name;
            savedUser.Email = user.Email ?? savedUser.Email;
            if (user.Password != null)
                savedUser.Password = GeneratePasswordHash(user.Password);
            await _userRepository.Update(savedUser);
        }

        private async Task<User> CreateUser(User user, Role role)
        {
            var candidate = await _userRepository
                .FirstOrDefault(u => u.Email == user.Email);
            if (candidate != null)
                return null;
            var passwordHash = GeneratePasswordHash(user.Password);
            user.Role = role.Name;
            user.Password = passwordHash;
            return await _userRepository.Create(user);
        }


        private static string GeneratePasswordHash(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        /// <summary>
        /// Returns the user Id from a ClaimsPrincipal object.
        /// </summary>
        /// <param name="user">The <see cref="ClaimsPrincipal"/> instance of current user's claims.</param>
        /// <returns>The user Id.</returns>
        public static Guid GetUserId(ClaimsPrincipal user)
        {
            var id = user.FindFirst("id")?.Value;
            if (id == null)
                throw new SecurityTokenValidationException(nameof(id));
            return Guid.Parse(id);
        }

        /// <summary>
        /// Checks if the current user has access to user with Id.
        /// </summary>
        /// <param name="user">The <see cref="ClaimsPrincipal"/> instance of current user's claims.</param>
        /// <param name="id">The user Id for check.</param>
        /// <returns>The user Id.</returns>
        public static bool IsHasAccess(ClaimsPrincipal user, Guid? id)
        {
            return user.IsInRole(RolesConstants.Admin) || GetUserId(user) == id;
        }
    }
}