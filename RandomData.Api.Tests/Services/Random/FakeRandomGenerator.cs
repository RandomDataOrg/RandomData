using System.Collections.Generic;
using RandomData.Api.Services.Random;

namespace RandomData.Api.Tests.Services.Random
{
    public class FakeRandomGenerator : IRandomGenerator
    {
        private readonly Queue<byte[]> _providedBytes;
        private readonly Queue<double> _providedDoubles;
        private readonly Queue<int> _providedInts;

        public FakeRandomGenerator(IEnumerable<int> ints = null, IEnumerable<byte[]> bytes = null,
            IEnumerable<double> doubles = null)
        {
            _providedInts = new Queue<int>(ints ?? new List<int>());
            _providedBytes = new Queue<byte[]>(bytes ?? new List<byte[]>());
            _providedDoubles = new Queue<double>(doubles ?? new List<double>());
        }

        public int Next(int min, int max)
        {
            return _providedInts.Dequeue();
        }
    }
}