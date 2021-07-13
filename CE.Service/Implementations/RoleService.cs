using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Threading.Tasks;
using CE.DataAccess.Models;
using CE.Repository;
using CE.Repository.Interfaces;
using CE.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CE.Service.Implementations
{
    public class RoleService : IRoleService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IGenericRepository<Role> _roleRepository;

        public RoleService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork as UnitOfWork;
            _roleRepository = _unitOfWork?.RoleRepository;
        }

        #region CREATE
        public async Task<ActionResult<Role>> Create(ClaimsPrincipal claims, Role item)
        {
            if (!UserService.IsHasAccess(claims))
                return new ForbidResult();

            var role = await _roleRepository.FirstOrDefault(r => r.Name == item.Name);
            if (role != null)
                return new BadRequestObjectResult($"The role named '{item.Name}' already exists.");

            await _roleRepository.Create(item);
            return new OkObjectResult(item);
        }
        #endregion CREATE

        #region GET
        public async Task<ActionResult<Role>> GetOne(
            ClaimsPrincipal claims, Guid id, 
            params Expression<Func<Role, object>>[] includeProperties)
        {
            if (!UserService.IsHasAccess(claims))
                return new ForbidResult();

            var role = await _roleRepository.GetById(id, includeProperties);
            return role != null ? new OkObjectResult(role) : new NotFoundObjectResult(new Role {Id = id});
        }

        public async Task<ActionResult<IEnumerable<Role>>> GetAll(
            ClaimsPrincipal claims = null, 
            Expression<Func<Role, bool>> filter = null, 
            Func<IQueryable<Role>, IOrderedQueryable<Role>> orderBy = null,
            params Expression<Func<Role, object>>[] includeProperties)
        {
            if (!UserService.IsHasAccess(claims))
                return new ForbidResult();

            var roles = await _roleRepository.GetAll(filter, orderBy, includeProperties);
            return new OkObjectResult(roles.ToList());
        }
        #endregion GET

        #region UPDATE
        public async Task<ActionResult<Role>> Update(ClaimsPrincipal claims, Role item)
        {
            if (!UserService.IsHasAccess(claims))
                return new ForbidResult();

            var role = await _roleRepository.FirstOrDefault(r => r.Name == item.Name);
            if (role != null)
                return new BadRequestObjectResult($"The role named '{item.Name}' already exists.");

            role = await _roleRepository.FirstOrDefault(r => r.Id == item.Id);
            if (role == null)
                return new NotFoundObjectResult(new Role {Id = item.Id});

            var users = (await _roleRepository.GetById(item.Id, r => r.Users)).Users;
            var roleToCreate = new Role {Name = item.Name};

            await _roleRepository.Create(roleToCreate);

            foreach (var user in users)
            {
                user.Role = roleToCreate.Name;
                await _unitOfWork.UserRepository.Update(user);
            }

            await _roleRepository.Remove(role);
            return new OkObjectResult(roleToCreate);
        }
        #endregion UPDATE

        #region DELETE
        public async Task<IActionResult> Delete(ClaimsPrincipal claims, Guid id)
        {
            if (!UserService.IsHasAccess(claims))
                return new ForbidResult();

            var role = await _roleRepository.GetById(id, r => r.Users);
            if (role == null)
                return new NotFoundObjectResult(new Role {Id = id});

            if (role.Users.Count > 0)
                return new BadRequestObjectResult(
                    $"The '{role.Name}' role with id={{{id}}} cannot be deleted, because there are Users with this role.");

            await _roleRepository.Remove(role);

            return new NoContentResult();
        }
        #endregion DELETE
    }
}
