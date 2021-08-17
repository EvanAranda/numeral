using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net.Http;

namespace Numeral
{
    internal static class Constants
    {
        public const int MaxStackAlloc = 4;
        public static readonly ReadOnlyMemory<int> OneArray = new int[] { 1 };
        public static readonly ReadOnlyMemoryCache SmallArrays = new();
    }

    internal class ReadOnlyMemoryCache
    {
        private readonly ConcurrentDictionary<int, int[]> _items;
        public ReadOnlyMemoryCache()
        {
            _items = new ConcurrentDictionary<int, int[]>();
        }

        public ReadOnlyMemory<int> Get(int i)
        {
            if (!_items.TryGetValue(i, out var arr))
            {
                arr = new int[] { i };
                _items.TryAdd(i, arr);
            }

            return arr;
        }
    }

}
