using System;
using System.Net.Http;

namespace Claim
{
    public class GetRequest : Request<GetRequest>
    {
        internal GetRequest(string uri, Func<HttpClient> httpClientFactory) : base(HttpMethod.Get, uri, httpClientFactory) {}
    }
}
