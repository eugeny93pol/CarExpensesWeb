using System;
using CE.DataAccess;
using CE.Repository.Interfaces;

namespace CE.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationContext _context;

        public readonly UserRepository UserRepository;
        public readonly UserSettingsRepository UserSettingsRepository;
        public readonly RoleRepository RoleRepository;
        public readonly CarRepository CarRepository;
        public readonly CarSettingsRepository CarSettingsRepository;
        public readonly ActionTypeRepository ActionTypeRepository;
        public readonly CarActionRepository CarActionRepository; 

        public UnitOfWork(ApplicationContext context)
        {
            _context = context;
            UserRepository = new UserRepository(_context);
            UserSettingsRepository = new UserSettingsRepository(_context);
            RoleRepository = new RoleRepository(_context);
            CarRepository = new CarRepository(_context);
            CarSettingsRepository = new CarSettingsRepository(_context);
            ActionTypeRepository = new ActionTypeRepository(_context);
            CarActionRepository = new CarActionRepository(_context);
        }

        public void Dispose()
        {
            _context?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
