using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RandomData.Api.NumberGenerators.Dto;

namespace RandomData.Api.NumberGenerators.Generators
{
    public class NumberGenerator : INumberGenerator
    {
        [ThreadStatic]
        private static Random _random;

        private static Random Rng => _random ??= new Random();

        public int GetRandom(NumberParameters parameters)
        {
            var length = Rng.Next(parameters.MinLength, parameters.MaxLength + 1);

            var result = new string(Enumerable.Repeat(parameters.AllowedDigits, length)
                .Select(d => d[Rng.Next(d.Length)]).ToArray());

            return int.Parse(result);
        }
    }
}
