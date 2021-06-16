using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CE.DataAccess;
using CE.Repository;
using CE.Service.Interfaces;

namespace CE.Service.Implementations
{
    public class CarActionService : BaseService<CarAction>, ICarActionService
    {
        public CarActionService(IRepository<CarAction> repository) : base(repository)
        {
        }

        public override async Task<CarAction> Create(CarAction item)
        {
            await CheckDateAndMileage(item);
            return await base.Create(item);
        }

        public override async Task Update(CarAction item)
        {
            await CheckDateAndMileage(item);
            await base.Update(item);
        }

        public async Task UpdatePartial(CarAction savedAction, CarAction action)
        {
            savedAction.Type = action.Type ?? savedAction.Type;
            savedAction.Mileage = action.Mileage ?? savedAction.Mileage;
            savedAction.Date = action.Date ?? savedAction.Date;
            savedAction.Description = action.Description ?? savedAction.Description;
            savedAction.Amount = action.Amount ?? savedAction.Amount;

            await CheckDateAndMileage(savedAction);

            await Repository.Update(savedAction);
        }


        private async Task CheckDateAndMileage(CarAction action)
        {
            var actions = await Repository.GetAll(a => a.CarId == action.CarId);
            var actionsList = actions.ToList();

            var beforeActions = actionsList
                .Where(a => a.Date < action.Date && a.Id != action.Id)
                .OrderByDescending(a => a.Date).ToList();

            var afterActions = actionsList
                .Where(a => a.Date >= action.Date && a.Id != action.Id)
                .OrderBy(a => a.Date).ToList();

            if (beforeActions.Any() && beforeActions.First().Mileage > action.Mileage ||
                afterActions.Any() && afterActions.First().Mileage < action.Mileage)
                throw new InvalidDataException(
                    $"Invalid mileage {action.Mileage} for date {action.Date}.");
        }
    }
}
