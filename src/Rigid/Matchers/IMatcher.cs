using System.Reflection;
using Newtonsoft.Json.Linq;

namespace Rigid.Matchers
{
    public class MatchingResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
    }

    public interface IMatcher
    {
        MatchingResult Match(PropertyInfo expectedProperty, JToken actualValue);
    }
}
