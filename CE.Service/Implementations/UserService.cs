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

        #region CRUD

        #region CREATE
        public async Task<ActionResult<User>> Create(ClaimsPrincipal claims, User item)
        {
            var roleUser = await _unitOfWork
                .RoleRepository
                .FirstOrDefault(r => r.Name == RolesConstants.User);

            var user = await CreateUser(item, roleUser);
            if (user == null)
                return new BadRequestObjectResult("That e-mail already registered.");

            await _unitOfWork.UserSettingsRepository.Create(new UserSettings(user.Id));
            return new OkObjectResult(user);
        }
        #endregion CREATE

        #region GET
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
        #endregion GET

        #region UPDATE
        public async Task<ActionResult<User>> Update(ClaimsPrincipal claims, User item)
        {
            if (!IsHasAccess(claims, item.Id))
                return new ForbidResult();

            var user = await _userRepository.FirstOrDefault(u => u.Id == item.Id);
            if (user == null)
                return new NotFoundObjectResult(item.Id);

            if (!IsHasAccess(claims) && user.Role != item.Role)
                return new ForbidResult();

            var validationResult = await ValidateUserUpdates(item);
            if (validationResult != null)
                return validationResult;

            item.Password = GeneratePasswordHash(item.Password);
            await _userRepository.Update(item);
            return new OkObjectResult(item);
        }


        public async Task<ActionResult<User>> UpdatePartial(ClaimsPrincipal claims, User item)
        {
            var user = await _userRepository.FirstOrDefault(u => u.Id == item.Id);
            if (user == null)
                return new NotFoundObjectResult(item.Id);
            
            user = UpdateUserInstance(user, item);
            return await Update(claims, user);
        }
        #endregion UPDATE

        #region DELETE
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
        #endregion DELETE

        #endregion CRUD

        #region AUTHENTICATE
        public async Task<User> Authenticate(string email, string password)
        {
            var user = await _userRepository.FirstOrDefault(u => u.Email == email);
            if (user != null && BCrypt.Net.BCrypt.Verify(password, user.Password))
                return user;

            return null;
        }
        #endregion AUTHENTICATE

        #region PRIVATE_TASKS
        private async Task<ActionResult> ValidateUserUpdates(User item)
        {
            var roles = (await _unitOfWork.RoleRepository.GetAll())
                .Select(r => r.Name)
                .ToArray();

            if (!roles.Contains(item.Role))
                return new BadRequestObjectResult($"The role '{item.Role}' does not exist.");

            var user = await _userRepository.FirstOrDefault(u => u.Id == item.Id);
            if (user.Email != item.Email)
            {
                user = await _userRepository.FirstOrDefault(u => u.Email == item.Email);
                if (user != null)
                    return new BadRequestObjectResult($"The email '{item.Email}' is already in use by another user.");
            }

            return null;
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
        #endregion PRIVATE_TASKS

        #region PUBLIC_STATIC
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
        /// Checks if the current user is an admin or has access to user with Id.
        /// </summary>
        /// <param name="user">The <see cref="ClaimsPrincipal"/> instance of current user's claims.</param>
        /// <param name="id">The user Id for check.</param>
        /// <returns>"true" - if the User is in role Admin, or if the id matches current User id, otherwise "false".</returns>
        public static bool IsHasAccess(ClaimsPrincipal user, Guid? id = null)
        {
            return user.IsInRole(RolesConstants.Admin) || GetUserId(user) == id;
        }
        #endregion PUBLIC_STATIC

        #region PRIVATE_STATIC
        private static string GeneratePasswordHash(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        private static User UpdateUserInstance(User savedUser, User user)
        {
            savedUser.Name = user.Name ?? savedUser.Name;
            savedUser.Email = user.Email ?? savedUser.Email;
            savedUser.Role = user.Role ?? savedUser.Role;
            savedUser.Password = user.Password ?? savedUser.Password;

            return savedUser;
        }
        #endregion PRIVATE_STATIC
    }
}