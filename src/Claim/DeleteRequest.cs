using System;
using System.Net.Http;

namespace Claim
{
    public class DeleteRequest : Request<DeleteRequest>
    {
        internal DeleteRequest(string uri, Func<HttpClient> httpClientFactory) : base(HttpMethod.Delete, uri, httpClientFactory) {}
    }
}
