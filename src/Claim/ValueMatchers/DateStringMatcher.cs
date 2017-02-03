using System.Reflection;
using Newtonsoft.Json.Linq;

namespace Claim.ValueMatchers
{
    public class DateStringMatcher : IPropertyValueMatcher
    {
        private readonly string _expected;

        public DateStringMatcher(string expected)
        {
            _expected = expected;
        }

        public MatchingResult Match(PropertyInfo expectedProperty, JToken actualValue)
        {
            var success = false;

            if (actualValue.Type == JTokenType.Date)
            {
                try
                {
                    var actualValueStr = (string)actualValue;
                    success = _expected == actualValueStr;
                }
                catch
                {
                }
            }

            return new MatchingResult
            {
                Success = success,
                Message = success ? string.Empty : $"The DateStringMatcher did not match the actual value: '{actualValue}'."
            };
        }
    }
}
