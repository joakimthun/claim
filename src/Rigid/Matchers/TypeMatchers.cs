using Newtonsoft.Json.Linq;

namespace Rigid.Matchers
{
    public class StringMatcher : IMatcher
    {
        public bool Match(JToken actualValue)
        {
            return actualValue.Type == JTokenType.String;
        }
    }

    public class ObjectMatcher : IMatcher
    {
        public bool Match(JToken actualValue)
        {
            return actualValue.Type == JTokenType.Object;
        }
    }

    public class ArrayMatcher : IMatcher
    {
        public bool Match(JToken actualValue)
        {
            return actualValue.Type == JTokenType.Array;
        }
    }

    public class IntMatcher : IMatcher
    {
        public bool Match(JToken actualValue)
        {
            return actualValue.Type == JTokenType.Integer;
        }
    }

    public class FloatMatcher : IMatcher
    {
        public bool Match(JToken actualValue)
        {
            return actualValue.Type == JTokenType.Float;
        }
    }

    public class BooleanMatcher : IMatcher
    {
        public bool Match(JToken actualValue)
        {
            return actualValue.Type == JTokenType.Boolean;
        }
    }

    public class NullMatcher : IMatcher
    {
        public bool Match(JToken actualValue)
        {
            return actualValue.Type == JTokenType.Null;
        }
    }

    public class DateMatcher : IMatcher
    {
        public bool Match(JToken actualValue)
        {
            return actualValue.Type == JTokenType.Date;
        }
    }

    public class UriMatcher : IMatcher
    {
        public bool Match(JToken actualValue)
        {
            return actualValue.Type == JTokenType.Uri;
        }
    }

    public class TimeSpanMatcher : IMatcher
    {
        public bool Match(JToken actualValue)
        {
            return actualValue.Type == JTokenType.TimeSpan;
        }
    }
}
