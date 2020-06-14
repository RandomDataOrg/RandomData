using System;

namespace RandomData.Api.Services.RandomService
{
    public interface IRandom
    {
        int Next();
        int Next(int max);
        int Next(int min, int max);
        void NextBytes(byte[] bytes);
        void NextBytes(Span<byte> bytes);
        double NextDouble();
    }
}