using System;
using System.Collections.Generic;
using System.Net.Http;
using Claim.ValueMatchers;

namespace Claim
{
    public static class Claim
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

        public static class ValueMatchers
        {
            public static StringTypeMatcher String => new StringTypeMatcher();
            public static ObjectTypeMatcher Object => new ObjectTypeMatcher();
            public static ArrayTypeMatcher Array => new ArrayTypeMatcher();
            public static IntTypeMatcher Int => new IntTypeMatcher();
            public static FloatTypeMatcher Float => new FloatTypeMatcher();
            public static BoolTypeMatcher Boolean => new BoolTypeMatcher();
            public static NullTypeMatcher Null => new NullTypeMatcher();
            public static DateTypeMatcher Date => new DateTypeMatcher();
            public static UriTypeMatcher Uri => new UriTypeMatcher();
            public static TimeSpanTypeMatcher TimeSpan => new TimeSpanTypeMatcher();

            public static RegexMatcher Regex(string regex) => new RegexMatcher(regex);
            public static ConfigurableJsonArrayMatcher ConfigurableJsonArrayMatcher(Array expected, bool matchLength) => 
                new ConfigurableJsonArrayMatcher(expected, matchLength);
        }
    }
}
