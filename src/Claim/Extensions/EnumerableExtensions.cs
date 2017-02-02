using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Claim.Extensions
{
    public static class EnumerableExtensions
    {
        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            if(source == null) throw new ArgumentNullException(nameof(source));
            if (action == null) throw new ArgumentNullException(nameof(action));

            foreach (var item in source)
            {
                action.Invoke(item);
            }
        }

        public static string ToCommaSeparatedList(this IEnumerable<string> source) => string.Join(",", source);
        public static string ToNewLineSeparatedList(this IEnumerable<string> source) => string.Join(Environment.NewLine, source);

        public static string JsonObjectPathJoin(this IEnumerable<string> source)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));

            var result = new StringBuilder();

            for (var i = 0; i < source.Count(); i++)
            {
                var item = source.ElementAt(i);

                result.Append(item);

                var nextItem = source.GetNextItem(i);
                if (nextItem != null && !nextItem.StartsWith("["))
                    result.Append(".");
            }

            return result.ToString();
        }

        private static string GetNextItem(this IEnumerable<string> source, int currentIndex)
        {
            var nextIndex = currentIndex + 1;
            if (nextIndex < source.Count())
                return source.ElementAt(nextIndex);

            return null;
        }
    }
}
