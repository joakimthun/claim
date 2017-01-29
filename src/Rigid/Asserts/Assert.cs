using System.Net.Http;

namespace Rigid.Asserts
{
    public abstract class Assert
    {
        public abstract Result Execute(HttpResponseMessage response);
        protected Result Passed() => Result.Passed();
        protected Result Failed(string message) => Result.Failed(message);
    }
}
