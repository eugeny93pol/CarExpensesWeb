using System;
using CE.DataAccess;
using CE.Repository.Interfaces;

namespace CE.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationContext _context;

        public readonly UserRepository UserRepository;
        public readonly RoleRepository RoleRepository;
        public readonly Repository<Car> CarRepository;

        public UnitOfWork(ApplicationContext context)
        {
            _context = context;
            UserRepository = new UserRepository(_context);
            RoleRepository = new RoleRepository(_context);
            CarRepository = new Repository<Car>(_context);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            _context?.Dispose();
        }
    }
}
