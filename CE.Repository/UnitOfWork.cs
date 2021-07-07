using System;
using CE.DataAccess.Models;
using CE.Repository.Interfaces;
using CE.Repository.Repositories;

namespace CE.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationContext _context;

        public readonly CarActionRepository CarActionRepository;
        public readonly IGenericRepository<Car> CarRepository;
        public readonly IGenericRepository<CarSettings> CarSettingsRepository;
        public readonly IGenericRepository<Role> RoleRepository;
        public readonly IGenericRepository<SparePart> SparePartRepository;
        public readonly IGenericRepository<User> UserRepository;
        public readonly IGenericRepository<UserSettings> UserSettingsRepository;

        public UnitOfWork(ApplicationContext context)
        {
            _context = context;
            CarActionRepository = new CarActionRepository(_context);
            CarRepository = new GenericRepository<Car>(_context);
            CarSettingsRepository = new GenericRepository<CarSettings>(_context);
            RoleRepository = new GenericRepository<Role>(_context);
            SparePartRepository = new GenericRepository<SparePart>(_context);
            UserRepository = new GenericRepository<User>(_context);
            UserSettingsRepository = new GenericRepository<UserSettings>(_context);
        }

        public void Dispose()
        {
            _context?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
