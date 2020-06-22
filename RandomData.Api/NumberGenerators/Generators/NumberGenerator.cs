using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RandomData.Api.NumberGenerators.Dto;

namespace RandomData.Api.NumberGenerators.Generators
{
    public class NumberGenerator
    {
        private readonly Random _random;

        public NumberGenerator()
        {
            _random = new Random();
        }

        public string GetRandom(NumberParameters parameters)
        {
            var length = _random.Next(parameters.MinLength, parameters.MaxLength + 1);

            var result = new string(Enumerable.Repeat(parameters.AllowedDigits, length)
                .Select(d => d[_random.Next(d.Length)]).ToArray());

            return result;
        }
    }
}
