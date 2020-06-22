using RandomData.Api.NumberGenerators.Dtos;

namespace RandomData.Api.NumberGenerators.Generators
{
    public interface INumberGenerator
    {
        public int GetRandom(NumberParameters parameters);
    }
}
