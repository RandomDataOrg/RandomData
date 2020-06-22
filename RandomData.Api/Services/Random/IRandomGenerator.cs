using System;

namespace RandomData.Api.Services.Random
{
    public interface IRandomGenerator
    {
        int Next();
        int Next(int max);
        int Next(int min, int max);
        void NextBytes(byte[] bytes);
        void NextBytes(Span<byte> bytes);
        double NextDouble();
    }
}