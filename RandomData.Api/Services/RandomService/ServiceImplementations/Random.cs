using System;

namespace RandomData.Api.Services.RandomService.ServiceImplementations
{
    public class Random : IRandom
    {
        private readonly System.Random _random = new System.Random();

        public int Next() => _random.Next();

        public int Next(int max) => _random.Next(max);

        public int Next(int min, int max) => _random.Next(min, max);

        public void NextBytes(byte[] bytes) => _random.NextBytes(bytes);

        public void NextBytes(Span<byte> bytes) => _random.NextBytes(bytes);

        public double NextDouble() => _random.NextDouble();
    }
}