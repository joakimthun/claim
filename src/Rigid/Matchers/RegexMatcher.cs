using System;
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

        public bool Match(JToken actualValue)
        {
            try
            {
                var actualValueStr = (string)actualValue;
                return _regex.IsMatch(actualValueStr);
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
