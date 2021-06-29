﻿using CE.DataAccess.Models;
using CE.Repository.Interfaces;

namespace CE.Repository.Repositories
{
    public class RoleRepository : GenericRepository<Role>, IRoleRepository
    {
        public RoleRepository(ApplicationContext context) : base(context)
        {
        }
    }
}
