using System;
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

        public int Next()
        {
            return _providedInts.Dequeue();
        }

        public int Next(int max)
        {
            return _providedInts.Dequeue();
        }

        public int Next(int min, int max)
        {
            return _providedInts.Dequeue();
        }

        public void NextBytes(byte[] bytes)
        {
            _providedBytes.Dequeue();
        }

        public void NextBytes(Span<byte> bytes)
        {
            _providedBytes.Dequeue();
        }

        public double NextDouble()
        {
            return _providedDoubles.Dequeue();
        }

        public void AppendInts(IEnumerable<int> ints)
        {
            foreach (var i in ints) _providedInts.Enqueue(i);
        }

        public void AppendBytes(IEnumerable<byte[]> bytes)
        {
            foreach (var b in bytes) _providedBytes.Enqueue(b);
        }

        public void AppendDoubles(IEnumerable<double> doubles)
        {
            foreach (var d in doubles) _providedDoubles.Enqueue(d);
        }
    }
}