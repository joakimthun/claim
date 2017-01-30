using System.Net.Http;

namespace Rigid
{
    public class Response
    {
        public HttpResponseMessage ResponseMessage { get; set; }
        public byte[] ResponseContent { get; set; }

        internal Response() {}
    }
}
