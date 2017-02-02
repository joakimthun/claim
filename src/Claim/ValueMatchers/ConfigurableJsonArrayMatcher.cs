using System;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json.Linq;
using Claim.Asserts;

namespace Claim.ValueMatchers
{
    public class ConfigurableJsonArrayMatcher : IPropertyValueMatcher
    {
        private readonly Array _expected;
        private readonly bool _matchLength;

        public ConfigurableJsonArrayMatcher(Array expected, bool matchLength)
        {
            _expected = expected;
            _matchLength = matchLength;
        }

        public MatchingResult Match(PropertyInfo expectedProperty, JToken actualValue)
        {
            if (actualValue.Type == JTokenType.Array)
            {
                var errors = JsonAssert.CompareArrays(expectedProperty, _expected, actualValue, _matchLength, false);

                return new MatchingResult
                {
                    Success = !errors.Any(),
                    Messages = errors
                };
            }
            else
            {
                return new MatchingResult
                {
                    Success = false,
                    Message = $"Type mismatch. Expected: 'Array', Actual: '{actualValue.Type}'"
                };
            }
        }
    }
}
