using System;
using System.Collections.Generic;
using System.Linq;

namespace Timesheets.DataGenerator
{
    public static class EnumerableExtensions
    {
        private static readonly Random Rnd = new Random();

        public static IEnumerable<T> RandomSubset<T>(this IEnumerable<T> source, int chance = 50)
        {
            return source.Where(x =>
            {
                var value = Rnd.Next(0, 100);
                return value < chance;
            });
        }

        public static T Random<T>(this IEnumerable<T> source)
        {
            var sourceList = source?.ToList() ?? new List<T>();
            if (!sourceList.Any())
            {
                throw new InvalidOperationException("Can't return random item: source collection is empty.");
            }

            var index = Rnd.Next(0, sourceList.Count);
            return sourceList[index];
        }
    }
}