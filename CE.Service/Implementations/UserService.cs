using System.Threading.Tasks;
using CE.DataAccess;
using CE.Repository;
using CE.Service.Interfaces;
using BC = BCrypt.Net.BCrypt;

namespace CE.Service.Implementations
{
    public class UserService : BaseService<User>, IUserService
    {
        public UserService(IRepository<User> repository) : base(repository)
        {
        }

        public async Task<User> CreateUser(User user, Role role)
        {
            var candidate = await Repository.FirstOrDefault(u => u.Email == user.Email);

            if (candidate != null)
            {
                return null;
            }

            var passwordHash = BC.HashPassword(user.Password);

            user.Role = role.Name;
            user.Password = passwordHash;

            return await Repository.Create(user);
        }

        public async Task<User> Authenticate(string email, string password)
        {
            var user = await Repository.FirstOrDefault(u => u.Email == email);

            if (user != null && BC.Verify(password, user.Password))
            {
                return user;
            }

            return null;
        }

    }
}