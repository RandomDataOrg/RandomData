using System;

namespace RandomData.Api.DateTimeGenerators
{
    public class RandomGenerator : IRandomGenerator
    {
        private readonly Random _random = new Random();

        public double NextDouble() => _random.NextDouble();
    }
}
