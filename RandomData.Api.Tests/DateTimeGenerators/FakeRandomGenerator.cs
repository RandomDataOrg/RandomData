using RandomData.Api.DateTimeGenerators;

namespace RandomData.Api.Tests.DateTimeGenerators
{
    public class FakeRandomGenerator : IRandomGenerator
    {
        public double NextDouble() => 1.0;
    }
}