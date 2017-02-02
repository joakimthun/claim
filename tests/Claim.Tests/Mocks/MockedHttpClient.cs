using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Claim.Tests.Mocks
{
    public class MockedHttpClient : HttpClient
    {
        private readonly HttpStatusCode _statusCode;
        private readonly HttpContent _content;
        private readonly IEnumerable<KeyValuePair<string, IEnumerable<string>>> _headers;

        public MockedHttpClient(HttpStatusCode statusCode, HttpContent content = null, IEnumerable<KeyValuePair<string, IEnumerable<string>>> headers = null)
        {
            _statusCode = statusCode;
            _content = content;
            _headers = headers;
        }

        public override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var response = new HttpResponseMessage(_statusCode)
            {
                Content = _content
            };

            if (_headers != null)
            {
                foreach (var header in _headers)
                {
                    response.Headers.Add(header.Key, header.Value);
                }
            }

            return Task.FromResult(response);
        }
    }
}
