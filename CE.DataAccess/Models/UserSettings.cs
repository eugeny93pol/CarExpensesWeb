using CE.DataAccess.Constants;

namespace CE.DataAccess
{
    public class UserSettings : BaseEntity
    {
        public string Language { get; set; }

        public string Theme { get; set; }

        public string MeasurementSystem { set; get; }

        public long UserId { get; set; }

        public UserSettings(long userId)
        {
            UserId = userId;
            Language = LanguagesConstants.English;
            Theme = ThemesConstants.Light;
            MeasurementSystem = MeasurementSystemConstants.Metric;
        }
    }
}
