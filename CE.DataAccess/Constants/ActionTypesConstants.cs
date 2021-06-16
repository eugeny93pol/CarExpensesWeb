namespace CE.DataAccess.Constants
{
    public static class ActionTypesConstants
    {
        public const string Mileage = "mileage";
        public const string Purchases = "purchases";
        public const string Refill = "refill";
        public const string Repair = "repair";

        public static readonly string[] Actions = new[] {Mileage, Mileage, Refill, Repair};
    }
}
