using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Rigid.Asserts;
using Rigid.Exceptions;

namespace Rigid
{
    public class Request
    {
        private readonly ICollection<Assert> _asserts = new List<Assert>();
        private readonly string _requestUri;
        private readonly HttpMethod _httpMethod;

        internal Request(string requestUri, HttpMethod httpMethod)
        {
            _requestUri = requestUri;
            _httpMethod = httpMethod;
        }

        public Request ExpectedStatus(HttpStatusCode expectedStatusCode)
        {
            _asserts.Add(new StatusCodeAssert(expectedStatusCode));
            return this;
        }

        public void Execute()
        {
            ExecuteAsync().Wait();
        }

        public async Task ExecuteAsync()
        {
            using (var client = new HttpClient())
            using (var response = await client.GetAsync(_requestUri))
            {
                var result = new List<Result>();
                foreach (var assert in _asserts)
                {
                    result.Add(assert.Execute(response));
                }

                var failed = result.Where(x => x.Status == ResultStatus.Failed).ToList();
                if(failed.Any())
                    throw new RequestFailedException(failed);
            }
        }
    }
}
