using System;

namespace RandomData.Api.Services.RandomService
{
    public interface IRandom
    {
        int Next();
        int Next(int max);
        int Next(int min, int max);
        void NextBytes(Byte[] bytes);
        void NextBytes(Span<Byte> bytes);
        double NextDouble();
    }
}