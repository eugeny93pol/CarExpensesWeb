using System;

namespace CE.DataAccess.Dtos
{
    public class GetUserSettingsDto
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public Guid? DefaultCarId { set; get; }
        
        public string Language { get; set; }

        public string Theme { get; set; }

        public string MeasurementSystem { set; get; }
    }
}