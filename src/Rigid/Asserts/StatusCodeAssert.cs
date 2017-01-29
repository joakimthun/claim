using System.Net;

namespace Rigid.Asserts
{
    public class StatusCodeAssert : Assert
    {
        private readonly HttpStatusCode _expectedStatusCode;

        public StatusCodeAssert(HttpStatusCode expectedStatusCode)
        {
            _expectedStatusCode = expectedStatusCode;
        }

        public override Result Execute(Response response)
        {
            if(response.ResponseMessage.StatusCode != _expectedStatusCode)
                return Failed<StatusCodeAssert>($"Expected status '{_expectedStatusCode}' but got status '{response.ResponseMessage.StatusCode}'.");

            return Passed<StatusCodeAssert>();
        }
    }
}
