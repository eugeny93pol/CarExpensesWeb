using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Threading.Tasks;
using CE.DataAccess.Constants;
using CE.DataAccess.Dtos;
using CE.DataAccess.Models;
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
        private readonly IGenericRepository<User> _userRepository;
        private readonly IUserSettingsService _userSettingsService;

        public UserService(IUnitOfWork unitOfWork, IUserSettingsService userSettingsService)
        {
            _unitOfWork = unitOfWork as UnitOfWork;
            _userRepository = _unitOfWork?.UserRepository;
            _userSettingsService = userSettingsService;
        }

        #region CRUD

        #region CREATE
        public async Task<ActionResult<UserDto>> Create(ClaimsPrincipal claims, CreateUserDto userDto)
        {
            var roleUser = await _unitOfWork
                .RoleRepository
                .FirstOrDefault(r => r.Name == RolesConstants.User);

            var user = await CreateUser(userDto.AsDbModel(), roleUser);
            if (user == null)
                return new BadRequestObjectResult("That e-mail already registered.");

            await _userSettingsService.Create(user.Id);
            return new OkObjectResult(user.AsDto());
        }
        #endregion CREATE

        #region GET
        public async Task<ActionResult<UserDto>> GetOne(ClaimsPrincipal claims, Guid id,
            params Expression<Func<User, object>>[] includeProperties)
        {
            if (!IsHasAccess(claims, id))
                return new ForbidResult();

            var user = await _userRepository.GetById(id, includeProperties);
            return user != null ? new OkObjectResult(user.AsDto()) : new NotFoundObjectResult(id);
        }

        public async Task<ActionResult<IEnumerable<UserDto>>> GetAll(ClaimsPrincipal claims, 
            Expression<Func<User, bool>> filter = null, 
            Func<IQueryable<User>, IOrderedQueryable<User>> orderBy = null, 
            params Expression<Func<User, object>>[] includeProperties)
        {
            if (!IsHasAccess(claims))
                return new ForbidResult();

            var users = await _userRepository.GetAll(filter, orderBy, includeProperties);
            var usersDto = users.ToList().Select(user => user.AsDto());
            return new OkObjectResult(usersDto);
        }
        #endregion GET

        #region UPDATE
        public async Task<ActionResult> Update(ClaimsPrincipal claims, UpdateUserDto userDto)
        {
            if (!IsHasAccess(claims, userDto.Id))
                return new ForbidResult();

            var user = await _userRepository.FirstOrDefault(u => u.Id == userDto.Id);
            if (user == null)
                return new NotFoundObjectResult(userDto.Id);

            if (!IsHasAccess(claims) && user.Role != userDto.Role)
                return new ForbidResult();

            var validationResult = await ValidateUserUpdates(user, userDto.AsDbModel());
            if (validationResult != null)
                return validationResult;

            if (!VerifyPassword(userDto.Password, user.Password))
                return new BadRequestObjectResult("The password is wrong.");

            userDto.Password = userDto.NewPassword != null
                ? GeneratePasswordHash(userDto.NewPassword)
                : user.Password;

            user = userDto.AsDbModel();
            await _userRepository.Update(user);
            return new NoContentResult();
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
        public async Task<UserDto> Authenticate(string email, string password)
        {
            var user = await _userRepository.FirstOrDefault(u => u.Email == email);
            if (user != null && VerifyPassword(password, user.Password))
                return user.AsDto();

            return null;
        }
        #endregion AUTHENTICATE

        #region PRIVATE_TASKS
        private async Task<ActionResult> ValidateUserUpdates(User savedUser, User userToUpdate)
        {
            var roles = (await _unitOfWork.RoleRepository.GetAll())
                .Select(r => r.Name)
                .ToArray();

            if (!roles.Contains(userToUpdate.Role))
                return new BadRequestObjectResult("The provided role does not exist.");

            if (savedUser.Email == userToUpdate.Email) return null;

            var user = await _userRepository.FirstOrDefault(u => u.Email == userToUpdate.Email);
            return user != null
                ? new BadRequestObjectResult("That e-mail already registered.")
                : null;
        }

        private async Task<User> CreateUser(User user, Role role)
        {
            var candidate = await _userRepository
                .FirstOrDefault(u => u.Email == user.Email);

            if (candidate != null)
                return null;

            user.Role = role.Name;
            user.Password = GeneratePasswordHash(user.Password);

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

        private static bool VerifyPassword(string password, string hash)
        {
            return BCrypt.Net.BCrypt.Verify(password, hash);
        }

        private static string GeneratePasswordHash(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }
        #endregion PRIVATE_STATIC
    }
}