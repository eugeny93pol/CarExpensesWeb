namespace CE.DataAccess.Constants
{
    public static class CarActionTypesConstants
    {
        public const string Default = "Action";
        public const string Mileage = "Mileage";
        public const string Purchases = "purchases";
        public const string Refill = "Refill";
        public const string Repair = "Repair";

        public static readonly string[] Actions = new[] {Mileage, Mileage, Refill, Repair};
    }
}
