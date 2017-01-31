using System;
using System.Net.Http;
using System.Text.RegularExpressions;
using Rigid.Matchers;

namespace Rigid
{
    public static class Rigid
    {
        public static GetRequest Get(string uri, Func<HttpClient> httpClientFactory = null) => new GetRequest(uri, httpClientFactory);
        public static PostRequest Post(string uri, Func<HttpClient> httpClientFactory = null) => new PostRequest(uri, httpClientFactory);

        public static class Matchers
        {
            public static StringMatcher String => new StringMatcher();
            public static ObjectMatcher Object => new ObjectMatcher();
            public static ArrayMatcher Array => new ArrayMatcher();
            public static IntMatcher Int => new IntMatcher();
            public static FloatMatcher Float => new FloatMatcher();
            public static BooleanMatcher Boolean => new BooleanMatcher();
            public static NullMatcher Null => new NullMatcher();
            public static DateMatcher Date => new DateMatcher();
            public static UriMatcher Uri => new UriMatcher();
            public static TimeSpanMatcher TimeSpan => new TimeSpanMatcher();
            public static RegexMatcher Regex(Regex r) => new RegexMatcher(r);
        }
    }
}
