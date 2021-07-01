using System.Threading.Tasks;
using CE.DataAccess.Models;
using CE.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CE.Repository.Repositories
{
    public class CarActionRepository<T> : GenericRepository<T>, ICarActionRepository where T : CarAction
    {
        public readonly DbSet<CarActionRefill> Refills;
        public readonly DbSet<CarActionRepair> Repairs;
        

        public CarActionRepository(ApplicationContext context) : base(context)
        {
            Refills = context.Set<CarActionRefill>();
            Repairs = context.Set<CarActionRepair>();
        }

        public async Task<CarActionRefill> Create(CarActionRefill item)
        {
            await Refills.AddAsync(item);
            await Context.SaveChangesAsync();
            return item;
        }

        public async Task<CarActionRepair> Create(CarActionRepair item)
        {
            await Repairs.AddAsync(item);
            await Context.SaveChangesAsync();
            return item;
        }
    }
}
