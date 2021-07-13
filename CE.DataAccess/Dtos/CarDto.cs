using System;
using System.Collections.Generic;
using CE.DataAccess.Models;

namespace CE.DataAccess.Dtos
{
    public class CarDto
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public string Brand { get; set; }

        public string Model { get; set; }

        public ushort? Year { get; set; }

        public uint? Mileage { get; set; }

        public string Vin { get; set; }

        public CarSettings Settings { get; set; }

        public ICollection<CarAction> Actions { get; set; }
    }
}