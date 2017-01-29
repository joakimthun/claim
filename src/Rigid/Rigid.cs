using System;
using System.Net.Http;

namespace Rigid
{
    public static class Rigid
    {
        public static GetRequest Get(string uri, Func<HttpClient> httpClientFactory = null) => new GetRequest(uri, httpClientFactory);
        public static PostRequest Post(string uri, Func<HttpClient> httpClientFactory = null) => new PostRequest(uri, httpClientFactory);
    }
}
