using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Rigid.Extensions
{
    public static class ReflectionExtensions
    {
        public static IReadOnlyCollection<PropertyInfo> GetProperties(this object obj)
        {
            return obj.GetType().GetRuntimeProperties().ToList();
        }
    }
}
