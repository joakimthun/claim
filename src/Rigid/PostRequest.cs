using System;
using System.Net.Http;
using Newtonsoft.Json;

namespace Rigid
{
    public class PostRequest : Request<PostRequest>
    {
        private const string ApplicationJsonContentType = "application/json";

        private readonly string _contentType;
        private readonly HttpContent _content;

        internal PostRequest(string uri, string jsonContent, Func<HttpClient> httpClientFactory) :
            base(HttpMethod.Post, uri, httpClientFactory)
        {
            _contentType = ApplicationJsonContentType;
            _content = new StringContent(jsonContent);
        }

        internal PostRequest(string uri, object jsonContent, Func<HttpClient> httpClientFactory) :
            base(HttpMethod.Post, uri, httpClientFactory)
        {
            _contentType = ApplicationJsonContentType;
            _content = new StringContent(JsonConvert.SerializeObject(jsonContent));
        }

        internal PostRequest(string uri, HttpContent content, string contentType, Func<HttpClient> httpClientFactory) :
            base(HttpMethod.Post, uri, httpClientFactory)
        {
            _contentType = contentType;
            _content = content;
        }

        protected override void BeforeSend(HttpRequestMessage httpRequest)
        {
            httpRequest.Headers.TryAddWithoutValidation("Content-Type", _contentType);
            httpRequest.Content = _content;
        }
    }
}
