using System;
using CE.DataAccess.Models;
using CE.Repository.Interfaces;
using CE.Repository.Repositories;

namespace CE.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationContext _context;

        public readonly IGenericRepository<CarAction> CarActionRepository;
        public readonly CarActionTypeRepository CarActionTypeRepository;
        public readonly CarRepository CarRepository;
        public readonly CarSettingsRepository CarSettingsRepository;
        public readonly RoleRepository RoleRepository;
        public readonly UserRepository UserRepository;
        public readonly UserSettingsRepository UserSettingsRepository;

        public UnitOfWork(ApplicationContext context)
        {
            _context = context;
            CarActionRepository = new GenericRepository<CarAction>(_context);
            CarActionTypeRepository = new CarActionTypeRepository(_context);
            CarRepository = new CarRepository(_context);
            CarSettingsRepository = new CarSettingsRepository(_context);
            RoleRepository = new RoleRepository(_context);
            UserRepository = new UserRepository(_context);
            UserSettingsRepository = new UserSettingsRepository(_context);
        }

        public void Dispose()
        {
            _context?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
