namespace RandomData.Api.Services.Random
{
    public interface IRandomGenerator
    {
        int Next(int min, int max);
    }
}