using System;
using System.Reflection;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;

namespace Rigid.Matchers
{
    public class RegexMatcher : IMatcher
    {
        private readonly Regex _regex;

        public RegexMatcher(Regex regex)
        {
            _regex = regex;
        }

        public MatchingResult Match(PropertyInfo expectedProperty, JToken actualValue)
        {
            var success = false;

            try
            {
                var actualValueStr =(string) actualValue;
                success = _regex.IsMatch(actualValueStr);
            }
            catch
            {
            }

            return new MatchingResult
            {
                Success = success,
                Message = success ? string.Empty : $"The RegexMatcher did not match the actual value: '{actualValue}'."
            };
        }
    }
}
