using System;
using System.Collections.Generic;

namespace Rigid.Extensions
{
    public static class EnumerableExtensions
    {
        public static string ToCommaSeparatedList(this IEnumerable<string> source) => string.Join(",", source);
        public static string ToNewLineSeparatedList(this IEnumerable<string> source) => string.Join(Environment.NewLine, source);
    }
}
