using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Threading.Tasks;
using CE.DataAccess.Dtos;
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
        public async Task<ActionResult<GetRoleDto>> Create(ClaimsPrincipal claims, CreateRoleDto dto)
        {
            if (!UserService.IsHasAccess(claims))
                return new ForbidResult();

            var role = await _roleRepository.FirstOrDefault(r => r.Name == dto.Name);
            if (role != null)
                return new BadRequestObjectResult($"The role named '{dto.Name}' already exists.");

            role = dto.AsDbModel();
            await _roleRepository.Create(role);
            return new OkObjectResult(role.AsDto());
        }
        #endregion CREATE

        #region GET
        public async Task<ActionResult<GetRoleDto>> GetOne(ClaimsPrincipal claims, Guid id, 
            params Expression<Func<Role, object>>[] includeProperties)
        {
            if (!UserService.IsHasAccess(claims))
                return new ForbidResult();

            var role = await _roleRepository.GetById(id, includeProperties);
            return role != null ? new OkObjectResult(role.AsDto()) : new NotFoundObjectResult(new Role {Id = id});
        }

        public async Task<ActionResult<IEnumerable<GetRoleDto>>> GetAll(
            ClaimsPrincipal claims = null, 
            Expression<Func<Role, bool>> filter = null, 
            Func<IQueryable<Role>, IOrderedQueryable<Role>> orderBy = null,
            params Expression<Func<Role, object>>[] includeProperties)
        {
            if (!UserService.IsHasAccess(claims))
                return new ForbidResult();

            var roles = await _roleRepository.GetAll(filter, orderBy, includeProperties);
            return new OkObjectResult(roles.ToList().Select(r => r.AsDto()));
        }
        #endregion GET

        #region UPDATE
        public async Task<ActionResult> Update(ClaimsPrincipal claims, UpdateRoleDto dto)
        {
            if (!UserService.IsHasAccess(claims))
                return new ForbidResult();

            var role = await _roleRepository.FirstOrDefault(r => r.Name == dto.Name);
            if (role != null)
                return new BadRequestObjectResult($"The role named '{dto.Name}' already exists.");

            role = await _roleRepository.FirstOrDefault(r => r.Id == dto.Id);
            if (role == null)
                return new NotFoundObjectResult(new Role {Id = dto.Id});

            var users = (await _roleRepository.GetById(dto.Id, r => r.Users)).Users;
            var roleToCreate = new Role {Name = dto.Name};

            await _roleRepository.Create(roleToCreate);

            foreach (var user in users)
            {
                user.Role = roleToCreate.Name;
                await _unitOfWork.UserRepository.Update(user);
            }

            await _roleRepository.Remove(role);
            return new NoContentResult();
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
