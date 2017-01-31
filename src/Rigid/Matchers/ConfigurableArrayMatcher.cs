using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;

namespace Rigid.Matchers
{
    public class ConfigurableArrayMatcher : IMatcher
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
