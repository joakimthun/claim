using System.Collections.Generic;

namespace Rigid.Extensions
{
    public static class EnumerableExtensions
    {
        public static string ToCommaSeparatedList(this IEnumerable<string> source) => string.Join(",", source);
    }
}
