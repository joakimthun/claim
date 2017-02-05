using System;
using System.Net.Http;

namespace Claim
{
    public static class Claims
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
    }
}
