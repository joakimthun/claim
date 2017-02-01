using System;
using System.Collections.Generic;
using System.Reflection;
using Newtonsoft.Json.Linq;

namespace Rigid.ValueMatchers
{
    public class ConfigurableArrayMatcher : IPropertyValueMatcher
    {
        private readonly IEnumerable<object> _expected;
        private readonly bool _matchLength;

        public ConfigurableArrayMatcher(IEnumerable<object> expected, bool matchLength)
        {
            _expected = expected;
            _matchLength = matchLength;
        }

        public MatchingResult Match(PropertyInfo expectedProperty, JToken actualValue)
        {
            //if (actualValue.Type != JTokenType.Array)
            //    return false;

            throw new NotImplementedException();
        }
    }
}
