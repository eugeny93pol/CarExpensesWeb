using CE.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CE.Service
{
    public interface IUserService : IBaseService<User>
    {
        Task<User> CreateUser(User item, Role role);

        Task<User> Authenticate(string email, string password);
        
    }
}
