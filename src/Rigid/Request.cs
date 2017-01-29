using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using Rigid.Asserts;
using Rigid.Exceptions;

namespace Rigid
{
    public abstract class Request<TRequest> where TRequest : Request<TRequest>
    {
        private readonly HttpClient _httpClient;
        private readonly HttpRequestMessage _httpRequest;

        protected ICollection<Assert> Asserts = new List<Assert>();

        public TRequest AssertStatus(HttpStatusCode expectedStatusCode)
        {
            Asserts.Add(new StatusCodeAssert(expectedStatusCode));
            return this as TRequest;
        }

        public void Execute()
        {
            BeforeSend(_httpRequest);
            var response = Send();

            var result = Asserts.Select(a => a.Execute(response));

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
            var response = _httpClient.SendAsync(_httpRequest).Result;

            return new Response
            {
                ResponseMessage = response,
                ResponseContent = new MemoryStream(response.Content.ReadAsByteArrayAsync().Result, false)
            };
        }

        private static HttpClient DefaultHttpClientFactory() => new HttpClient();
    }
}
