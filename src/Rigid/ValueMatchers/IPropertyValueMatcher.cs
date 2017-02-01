using System.Reflection;
using Newtonsoft.Json.Linq;

namespace Rigid.ValueMatchers
{
    public class MatchingResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
    }

    public interface IPropertyValueMatcher
    {
        MatchingResult Match(PropertyInfo expectedProperty, JToken actualValue);
    }
}
