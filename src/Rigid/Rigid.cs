using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.RegularExpressions;
using Rigid.Matchers;

namespace Rigid
{
    public static class Rigid
    {
        public static GetRequest Get(string uri, Func<HttpClient> httpClientFactory = null) => new GetRequest(uri, httpClientFactory);

        public static ContentRequest Post(string uri, HttpContent content, string contentType, Func<HttpClient> httpClientFactory = null) => 
            new ContentRequest(HttpMethod.Post, uri, content, contentType, httpClientFactory);
        public static ContentRequest Post(string uri, string jsonContent, Func<HttpClient> httpClientFactory = null) => 
            new ContentRequest(HttpMethod.Post, uri, jsonContent, httpClientFactory);
        public static ContentRequest Post(string uri, object jsonContent, Func<HttpClient> httpClientFactory = null) => 
            new ContentRequest(HttpMethod.Post, uri, jsonContent, httpClientFactory);

        public static DeleteRequest Delete(string uri, Func<HttpClient> httpClientFactory = null) => new DeleteRequest(uri, httpClientFactory);

        public static ContentRequest Put(string uri, HttpContent content, string contentType, Func<HttpClient> httpClientFactory = null) =>
            new ContentRequest(HttpMethod.Put, uri, content, contentType, httpClientFactory);
        public static ContentRequest Put(string uri, string jsonContent, Func<HttpClient> httpClientFactory = null) =>
            new ContentRequest(HttpMethod.Put, uri, jsonContent, httpClientFactory);
        public static ContentRequest Put(string uri, object jsonContent, Func<HttpClient> httpClientFactory = null) =>
            new ContentRequest(HttpMethod.Put, uri, jsonContent, httpClientFactory);

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
            public static ConfigurableArrayMatcher ConfigurableArrayMatcher(IEnumerable<object> expected, bool matchLength) => 
                new ConfigurableArrayMatcher(expected, matchLength);
        }
    }
}
