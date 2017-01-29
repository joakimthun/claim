namespace Rigid.Asserts
{
    public abstract class Assert
    {
        public abstract Result Execute(Response response);
        protected Result Passed<TAssert>() where TAssert : Assert => Result.Passed<TAssert>();
        protected Result Failed<TAssert>(string message) where TAssert : Assert => Result.Failed<TAssert>(message);
    }
}
