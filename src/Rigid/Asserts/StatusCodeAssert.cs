using System.Net;
using System.Net.Http;

namespace Rigid.Asserts
{
    public class StatusCodeAssert : Assert
    {
        private readonly HttpStatusCode _expectedStatusCode;

        public StatusCodeAssert(HttpStatusCode expectedStatusCode)
        {
            _expectedStatusCode = expectedStatusCode;
        }

        public override Result Execute(HttpResponseMessage response)
        {
            if(response.StatusCode != _expectedStatusCode)
                return Failed("response.StatusCode != _expectedStatusCode");

            return Passed();
        }
    }
}
