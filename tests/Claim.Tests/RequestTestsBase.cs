using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;
using Claim.Tests.Mocks;

namespace Claim.Tests
{
    public class RequestTestsBase
    {
        protected static HttpClient CreateMockedJsonHttpClient(string content)
        {
            return CreateMockedHttpClient(HttpStatusCode.OK, new StringContent(content));
        }

        protected static HttpClient CreateMockedJsonHttpClient(object content)
        {
            return CreateMockedHttpClient(HttpStatusCode.OK, new StringContent(JsonConvert.SerializeObject(content)));
        }

        protected static HttpClient CreateMockedHttpClient(HttpStatusCode statusCode, HttpContent content = null, IEnumerable<KeyValuePair<string, IEnumerable<string>>> headers = null)
        {
            return new MockedHttpClient(statusCode, content, headers);
        }
    }
}
