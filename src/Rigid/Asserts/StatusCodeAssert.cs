using System.Net;

namespace Rigid.Asserts
{
    public class StatusCodeAssert : IAssert
    {
        private readonly HttpStatusCode _expectedStatusCode;

        public StatusCodeAssert(HttpStatusCode expectedStatusCode)
        {
            _expectedStatusCode = expectedStatusCode;
        }

        public Result Assert(Response response)
        {
            if(response.ResponseMessage.StatusCode != _expectedStatusCode)
                return Result.Failed<StatusCodeAssert>($"Expected status '{_expectedStatusCode}' but got status '{response.ResponseMessage.StatusCode}'.");

            return Result.Passed<StatusCodeAssert>();
        }
    }
}
