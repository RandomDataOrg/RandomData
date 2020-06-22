using System;

namespace RandomData.Api.Services.Random.ServiceImplementations
{
    public class RandomGenerator : IRandomGenerator
    {
        private readonly System.Random _random = new System.Random();

        public int Next()
        {
            return _random.Next();
        }

        public int Next(int max)
        {
            return _random.Next(max);
        }

        public int Next(int min, int max)
        {
            return _random.Next(min, max);
        }

        public void NextBytes(byte[] bytes)
        {
            _random.NextBytes(bytes);
        }

        public void NextBytes(Span<byte> bytes)
        {
            _random.NextBytes(bytes);
        }

        public double NextDouble()
        {
            return _random.NextDouble();
        }
    }
}