using System;
using CE.DataAccess.Models;
using CE.Repository.Interfaces;
using CE.Repository.Repositories;

namespace CE.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationContext _context;

        //public readonly IGenericRepository<CarActionRefill> RefillRepository;
        //public readonly IGenericRepository<CarActionRepair> RepairRepository;
        public readonly CarActionRepository<CarAction> CarActionRepository;

        public readonly IGenericRepository<CarActionType> CarActionTypeRepository;
        public readonly IGenericRepository<Car> CarRepository;
        public readonly IGenericRepository<CarSettings> CarSettingsRepository;
        public readonly IGenericRepository<Role> RoleRepository;
        public readonly IGenericRepository<User> UserRepository;
        public readonly IGenericRepository<UserSettings> UserSettingsRepository;

        public UnitOfWork(ApplicationContext context)
        {
            _context = context;
            //RefillRepository = new GenericRepository<CarActionRefill>(_context);
            //RepairRepository = new GenericRepository<CarActionRepair>(_context);
            CarActionRepository = new CarActionRepository<CarAction>(context);

            CarActionTypeRepository = new GenericRepository<CarActionType>(_context);
            CarRepository = new GenericRepository<Car>(_context);
            CarSettingsRepository = new GenericRepository<CarSettings>(_context);
            RoleRepository = new GenericRepository<Role>(_context);
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
