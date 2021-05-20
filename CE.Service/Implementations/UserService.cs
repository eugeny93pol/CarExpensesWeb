using BC = BCrypt.Net.BCrypt;
using CE.DataAccess;
using CE.Repository;
using System.Threading.Tasks;

namespace CE.Service
{
    public class UserService : BaseService<User>, IUserService
    {
        public UserService(IRepository<User> repository) : base(repository)
        {
        }

        public async Task<User> CreateUser(User user, Role role)
        {
            var candidate = await _repository.FirstOrDefault(u => u.Email == user.Email);

            if (candidate != null)
            {
                return null;
            }

            string passwordHash = BC.HashPassword(user.Password);

            user.Role = role.Name;
            user.Password = passwordHash;

            return await _repository.Create(user);
        }

        public async Task<User> Authenticate(string email, string password)
        {
            var user = await _repository.FirstOrDefault(
                u => u.Email == email);

            if (user == null)
            {
                return null;
            }

            return BC.Verify(password, user.Password) ? user : null;
        }

    }
}
