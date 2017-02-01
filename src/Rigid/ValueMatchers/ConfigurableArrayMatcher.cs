using System;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json.Linq;
using Rigid.Asserts;

namespace Rigid.ValueMatchers
{
    public class ConfigurableArrayMatcher : IPropertyValueMatcher
    {
        private readonly Array _expected;
        private readonly bool _matchLength;

        public ConfigurableArrayMatcher(Array expected, bool matchLength)
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
