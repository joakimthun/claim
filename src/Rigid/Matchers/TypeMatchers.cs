using System.Reflection;
using Newtonsoft.Json.Linq;

namespace Rigid.Matchers
{
    internal static class TypeMatchingResultHelper
    {
        public static MatchingResult CreateMatchingResult(JTokenType expectedType, JToken actualValue)
        {
            var success = actualValue.Type == expectedType;

            return new MatchingResult
            {
                Success = success,
                Message = success ? string.Empty : $"Type mismatch. Expected: '{expectedType}', Actual: '{actualValue.Type}'"
            };
        }
    }

    public class StringMatcher : IMatcher
    {
        public MatchingResult Match(PropertyInfo expectedProperty, JToken actualValue)
        {
            return TypeMatchingResultHelper.CreateMatchingResult(JTokenType.String, actualValue);
        }
    }

    public class ObjectMatcher : IMatcher
    {
        public MatchingResult Match(PropertyInfo expectedProperty, JToken actualValue)
        {
            return TypeMatchingResultHelper.CreateMatchingResult(JTokenType.Object, actualValue);
        }
    }

    public class ArrayMatcher : IMatcher
    {
        public MatchingResult Match(PropertyInfo expectedProperty, JToken actualValue)
        {
            return TypeMatchingResultHelper.CreateMatchingResult(JTokenType.Array, actualValue);
        }
    }

    public class IntMatcher : IMatcher
    {
        public MatchingResult Match(PropertyInfo expectedProperty, JToken actualValue)
        {
            return TypeMatchingResultHelper.CreateMatchingResult(JTokenType.Integer, actualValue);
        }
    }

    public class FloatMatcher : IMatcher
    {
        public MatchingResult Match(PropertyInfo expectedProperty, JToken actualValue)
        {
            return TypeMatchingResultHelper.CreateMatchingResult(JTokenType.Float, actualValue);
        }
    }

    public class BooleanMatcher : IMatcher
    {
        public MatchingResult Match(PropertyInfo expectedProperty, JToken actualValue)
        {
            return TypeMatchingResultHelper.CreateMatchingResult(JTokenType.Boolean, actualValue);
        }
    }

    public class NullMatcher : IMatcher
    {
        public MatchingResult Match(PropertyInfo expectedProperty, JToken actualValue)
        {
            return TypeMatchingResultHelper.CreateMatchingResult(JTokenType.Null, actualValue);
        }
    }

    public class DateMatcher : IMatcher
    {
        public MatchingResult Match(PropertyInfo expectedProperty, JToken actualValue)
        {
            return TypeMatchingResultHelper.CreateMatchingResult(JTokenType.Date, actualValue);
        }
    }

    public class UriMatcher : IMatcher
    {
        public MatchingResult Match(PropertyInfo expectedProperty, JToken actualValue)
        {
            return TypeMatchingResultHelper.CreateMatchingResult(JTokenType.Uri, actualValue);
        }
    }

    public class TimeSpanMatcher : IMatcher
    {
        public MatchingResult Match(PropertyInfo expectedProperty, JToken actualValue)
        {
            return TypeMatchingResultHelper.CreateMatchingResult(JTokenType.TimeSpan, actualValue);
        }
    }
}
