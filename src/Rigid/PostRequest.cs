using System;
using System.Net.Http;

namespace Rigid
{
    public class PostRequest : Request<PostRequest>
    {
        internal PostRequest(string uri, Func<HttpClient> httpClientFactory) : base(HttpMethod.Post, uri, httpClientFactory) {}
    }
}
