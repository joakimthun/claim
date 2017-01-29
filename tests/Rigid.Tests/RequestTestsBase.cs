using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using Rigid.Tests.Mocks;

namespace Rigid.Tests
{
    public class RequestTestsBase
    {
        protected static HttpClient CreateMockedHttpClient(HttpStatusCode statusCode, HttpContent content = null, IEnumerable<KeyValuePair<string, IEnumerable<string>>> headers = null)
        {
            return new MockedHttpClient(statusCode, content, headers);
        }
    }
}
