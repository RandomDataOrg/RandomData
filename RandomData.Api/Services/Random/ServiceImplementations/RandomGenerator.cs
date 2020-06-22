namespace RandomData.Api.Services.Random.ServiceImplementations
{
    public class RandomGenerator : IRandomGenerator
    {
        private readonly System.Random _random = new System.Random();

        public int Next(int min, int max)
        {
            return _random.Next(min, max);
        }
    }
}