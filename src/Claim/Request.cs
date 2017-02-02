using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using Claim.Asserts;
using Claim.Exceptions;

namespace Claim
{
    public abstract class Request<TRequest> where TRequest : Request<TRequest>
    {
        private readonly HttpClient _httpClient;
        private readonly HttpRequestMessage _httpRequest;

        protected ICollection<IAssert> Asserts = new List<IAssert>();

        public TRequest AssertStatus(HttpStatusCode expectedStatusCode)
        {
            AddAssert(new StatusCodeAssert(expectedStatusCode));
            return this as TRequest;
        }

        public TRequest AssertContainsHeader(string name, IEnumerable<string> values)
        {
            AddAssert(new ContainsHeaderAssert(name, values));
            return this as TRequest;
        }

        public TRequest AssertContainsHeader(string name, string value)
        {
            AddAssert(new ContainsHeaderAssert(name, new []{ value }));
            return this as TRequest;
        }

        public TRequest AssertJson(object expectedResponseStructure, PropertyComparison? propertyComparison = null)
        {
            AddAssert(new JsonAssert(expectedResponseStructure, propertyComparison));
            return this as TRequest;
        }

        public TRequest AssertJson(string expectedResponseStructure, PropertyComparison? propertyComparison = null)
        {
            AddAssert(new JsonAssert(expectedResponseStructure, propertyComparison));
            return this as TRequest;
        }

        public TRequest AddAssert(IAssert assert)
        {
            Asserts.Add(assert);
            return this as TRequest;
        }

        public void Execute()
        {
            BeforeSend(_httpRequest);
            var response = Send();

            var result = Asserts.Select(a => a.Assert(response));

            var failedAsserts = result.Where(x => x.Status == ResultStatus.Failed).ToList();
            if (failedAsserts.Any())
                throw new AssertFailedException(failedAsserts);
        }

        protected virtual void BeforeSend(HttpRequestMessage httpRequest) { }

        protected Request(HttpMethod httpMethod, string uri, Func<HttpClient> httpClientFactory)
        {
            _httpClient = (httpClientFactory ?? DefaultHttpClientFactory)();
            _httpRequest = new HttpRequestMessage(httpMethod, uri);
        }

        private Response Send()
        {
            // Use the CancellationToken override so we can mock the HttpClient by overriding SendAsync
            var response = _httpClient.SendAsync(_httpRequest, new CancellationToken()).Result;

            return new Response
            {
                ResponseMessage = response,
                ResponseContent = response?.Content?.ReadAsByteArrayAsync().Result
            };
        }

        private static HttpClient DefaultHttpClientFactory() => new HttpClient();
    }
}
