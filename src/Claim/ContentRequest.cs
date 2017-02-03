using System;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace Claim
{
    public class ContentRequest : Request<ContentRequest>
    {
        private const string ApplicationJsonContentType = "application/json";

        private readonly string _contentType;
        private readonly HttpContent _content;

        internal ContentRequest(HttpMethod method, string uri, string jsonContent, Func<HttpClient> httpClientFactory) :
            base(method, uri, httpClientFactory)
        {
            _contentType = ApplicationJsonContentType;
            _content = new StringContent(jsonContent);
        }

        internal ContentRequest(HttpMethod method, string uri, object jsonContent, Func<HttpClient> httpClientFactory) :
            base(method, uri, httpClientFactory)
        {
            _contentType = ApplicationJsonContentType;
            _content = new StringContent(JsonConvert.SerializeObject(jsonContent));
        }

        internal ContentRequest(HttpMethod method, string uri, HttpContent content, string contentType, Func<HttpClient> httpClientFactory) :
            base(method, uri, httpClientFactory)
        {
            _contentType = contentType;
            _content = content;
        }

        protected override void BeforeSend(HttpRequestMessage httpRequest)
        {
            httpRequest.Content = _content;
            httpRequest.Content.Headers.ContentType = new MediaTypeHeaderValue(_contentType);
        }
    }
}
