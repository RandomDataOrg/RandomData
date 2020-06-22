using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RandomData.Api.NumberGenerators.Dto;

namespace RandomData.Api.NumberGenerators.Generators
{
    public interface INumberGenerator
    {
        public int GetRandom(NumberParameters parameters);
    }
}
