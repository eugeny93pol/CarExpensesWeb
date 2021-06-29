using CE.DataAccess;
using CE.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CE.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        //private readonly ApplicationContext _context;
        //private readonly DbSet<User> _users;

        public UserRepository(ApplicationContext context) : base(context)
        {
            //_context = context;
            //_users = context.Set<User>();
        }
    }
}
