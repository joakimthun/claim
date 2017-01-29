using System.Net.Http;

namespace Rigid.Asserts
{
    public abstract class Assert
    {
        public abstract Result Execute(HttpResponseMessage response);
        protected Result Passed<TAssert>() where TAssert : Assert => Result.Passed<TAssert>();
        protected Result Failed<TAssert>(string message) where TAssert : Assert => Result.Failed<TAssert>(message);
    }
}
