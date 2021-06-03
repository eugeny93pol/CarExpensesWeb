using System.Threading.Tasks;
using CE.DataAccess;

namespace CE.Service.Interfaces
{
    public interface IUserService : IBaseService<User>
    {
        Task<User> CreateUser(User item, Role role);

        Task<User> Authenticate(string email, string password);

        Task UpdatePartial(User savedUser, User user);

        string GeneratePasswordHash(string password);
    }
}
