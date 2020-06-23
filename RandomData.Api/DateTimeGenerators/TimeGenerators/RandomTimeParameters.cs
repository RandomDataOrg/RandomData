namespace RandomData.Api.DateTimeGenerators.TimeGenerators
{
    public class RandomTimeParameters
    {
        public string MinTime { get; set; } = "06:00";
        public string MaxTime { get; set; } = "23:59";
        public RandomTimeFormats Format { get; set; } = RandomTimeFormats.Short;
    }
}
