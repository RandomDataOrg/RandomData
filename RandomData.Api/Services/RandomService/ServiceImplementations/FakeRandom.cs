using System;
using System.Collections.Generic;
using System.Linq;

namespace RandomData.Api.Services.RandomService.ServiceImplementations
{
    public class FakeRandom : IRandom
    {
        private readonly Queue<int> _providedInts;
        private readonly Queue<byte[]> _providedBytes;
        private readonly Queue<double> _providedDoubles;
        
        public FakeRandom(IEnumerable<int> ints = null, IEnumerable<byte[]> bytes = null, IEnumerable<double> doubles = null)
        {
            _providedInts = new Queue<int>(ints ?? new List<int>());
            _providedBytes = new Queue<byte[]>(bytes ?? new List<byte[]>());
            _providedDoubles = new Queue<double>(doubles ?? new List<double>());
        }

        public void AppendInts(IEnumerable<int> ints)
        {
            foreach (var i in ints)
            {
                _providedInts.Enqueue(i);
            }
        }

        public void AppendBytes(IEnumerable<byte[]> bytes)
        {
            foreach (var b in bytes)
            {
                _providedBytes.Enqueue(b);
            }
        }

        public void AppendDoubles(IEnumerable<double> doubles)
        {
            foreach (var d in doubles)
            {
                _providedDoubles.Enqueue(d);
            }
        }

        public int Next() => _providedInts.Dequeue();

        public int Next(int max) => _providedInts.Dequeue();

        public int Next(int min, int max) => _providedInts.Dequeue();

        public void NextBytes(byte[] bytes) => _providedBytes.Dequeue();

        public void NextBytes(Span<byte> bytes) => _providedBytes.Dequeue();

        public double NextDouble() => _providedDoubles.Dequeue();
    }
}