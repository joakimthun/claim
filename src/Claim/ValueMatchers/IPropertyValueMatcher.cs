using System.Collections.Generic;
using System.Reflection;
using Newtonsoft.Json.Linq;

namespace Claim.ValueMatchers
{
    public class MatchingResult
    {
        public bool Success { get; set; }

        public string Message
        {
            set
            {
                Messages = new[] { value };
            }
        }

        public IEnumerable<string> Messages { get; set; } = new List<string>();
    }

    public interface IPropertyValueMatcher
    {
        MatchingResult Match(PropertyInfo expectedProperty, JToken actualValue);
    }
}
