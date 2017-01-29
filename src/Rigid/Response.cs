using System.IO;
using System.Net.Http;

namespace Rigid
{
    public class Response
    {
        public HttpResponseMessage ResponseMessage { get; set; }
        public MemoryStream ResponseContent { get; set; }

        internal Response() {}
    }
}
