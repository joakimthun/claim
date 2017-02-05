using System;
using System.Reflection;
using Newtonsoft.Json.Linq;

namespace Claim.ValueMatchers
{
    public class DateMatcher : IPropertyValueMatcher
    {
        private readonly DateTime _expected;

        public DateMatcher(string expected)
        {
            _expected = DateTime.Parse(expected);
        }

        public DateMatcher(DateTime expected)
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
                    success = (DateTime)actualValue == _expected;
                }
                catch
                {
                }
            }

            return new MatchingResult
            {
                Success = success,
                Message = success ? string.Empty : $"The DateMatcher did not match. Actual value: '{actualValue}', Actual type: '{actualValue.Type}'"
            };
        }
    }
}
