using System;
using CE.DataAccess.Models;

namespace CE.WebAPI.RequestModels
{
    public class PatchCarAction
    {
        public string Type { get; set; }

        public uint? Mileage { get; set; }

        public DateTime? Date { get; set; }

        public decimal? Amount { get; set; }

        public string Description { get; set; }

        public CarAction GetAction()
        {
            return new()
            {
                //Type = this.Type,
                Mileage = this.Mileage,
                Date = this.Date,
                Description = this.Description,
                //Amount = this.Amount
            };
        }
    }
}
