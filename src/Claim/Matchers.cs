using Claim.ValueMatchers;
using System;

namespace Claim
{
    public static class Matchers
    {
        public static StringTypeMatcher String => new StringTypeMatcher();
        public static ObjectTypeMatcher Object => new ObjectTypeMatcher();
        public static ArrayTypeMatcher Array => new ArrayTypeMatcher();
        public static IntTypeMatcher Int => new IntTypeMatcher();
        public static FloatTypeMatcher Float => new FloatTypeMatcher();
        public static BoolTypeMatcher Boolean => new BoolTypeMatcher();
        public static NullTypeMatcher Null => new NullTypeMatcher();
        public static DateTypeMatcher Date => new DateTypeMatcher();
        public static DateStringMatcher DateString(string expected) => new DateStringMatcher(expected);
        public static UriTypeMatcher Uri => new UriTypeMatcher();
        public static TimeSpanTypeMatcher TimeSpan => new TimeSpanTypeMatcher();

        public static RegexMatcher Regex(string regex) => new RegexMatcher(regex);
        public static ConfigurableJsonArrayMatcher ConfigurableJsonArrayMatcher(Array expected, bool matchLength) =>
            new ConfigurableJsonArrayMatcher(expected, matchLength);
    }
}
