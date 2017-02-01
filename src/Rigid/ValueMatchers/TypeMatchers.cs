using System.Reflection;
using Newtonsoft.Json.Linq;

namespace Rigid.ValueMatchers
{
    internal static class TypeMatchingResultHelper
    {
        public static MatchingResult CreateMatchingResult(JTokenType expectedType, JToken actualValue)
        {
            var success = actualValue.Type == expectedType;

            return new MatchingResult
            {
                Success = success,
                Message = success ? string.Empty : $"Type mismatch. Expected: '{expectedType}', Actual: '{actualValue.Type}'."
            };
        }
    }

    public class StringTypeMatcher : IPropertyValueMatcher
    {
        public MatchingResult Match(PropertyInfo expectedProperty, JToken actualValue)
        {
            return TypeMatchingResultHelper.CreateMatchingResult(JTokenType.String, actualValue);
        }
    }

    public class ObjectTypeMatcher : IPropertyValueMatcher
    {
        public MatchingResult Match(PropertyInfo expectedProperty, JToken actualValue)
        {
            return TypeMatchingResultHelper.CreateMatchingResult(JTokenType.Object, actualValue);
        }
    }

    public class ArrayTypeMatcher : IPropertyValueMatcher
    {
        public MatchingResult Match(PropertyInfo expectedProperty, JToken actualValue)
        {
            return TypeMatchingResultHelper.CreateMatchingResult(JTokenType.Array, actualValue);
        }
    }

    public class IntTypeMatcher : IPropertyValueMatcher
    {
        public MatchingResult Match(PropertyInfo expectedProperty, JToken actualValue)
        {
            return TypeMatchingResultHelper.CreateMatchingResult(JTokenType.Integer, actualValue);
        }
    }

    public class FloatTypeMatcher : IPropertyValueMatcher
    {
        public MatchingResult Match(PropertyInfo expectedProperty, JToken actualValue)
        {
            return TypeMatchingResultHelper.CreateMatchingResult(JTokenType.Float, actualValue);
        }
    }

    public class BoolTypeMatcher : IPropertyValueMatcher
    {
        public MatchingResult Match(PropertyInfo expectedProperty, JToken actualValue)
        {
            return TypeMatchingResultHelper.CreateMatchingResult(JTokenType.Boolean, actualValue);
        }
    }

    public class NullTypeMatcher : IPropertyValueMatcher
    {
        public MatchingResult Match(PropertyInfo expectedProperty, JToken actualValue)
        {
            return TypeMatchingResultHelper.CreateMatchingResult(JTokenType.Null, actualValue);
        }
    }

    public class DateTypeMatcher : IPropertyValueMatcher
    {
        public MatchingResult Match(PropertyInfo expectedProperty, JToken actualValue)
        {
            return TypeMatchingResultHelper.CreateMatchingResult(JTokenType.Date, actualValue);
        }
    }

    public class UriTypeMatcher : IPropertyValueMatcher
    {
        public MatchingResult Match(PropertyInfo expectedProperty, JToken actualValue)
        {
            return TypeMatchingResultHelper.CreateMatchingResult(JTokenType.Uri, actualValue);
        }
    }

    public class TimeSpanTypeMatcher : IPropertyValueMatcher
    {
        public MatchingResult Match(PropertyInfo expectedProperty, JToken actualValue)
        {
            return TypeMatchingResultHelper.CreateMatchingResult(JTokenType.TimeSpan, actualValue);
        }
    }
}
