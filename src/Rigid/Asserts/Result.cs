using System;

namespace Rigid.Asserts
{
    public enum ResultStatus : byte
    {
        Passed,
        Failed
    }

    public class Result
    {
        public ResultStatus Status { get; private set; }
        public string Message { get; private set; }
        public Type AssertType { get; }

        public static Result Passed<TAssert>() where TAssert : IAssert => new Result(ResultStatus.Passed, "Passed", typeof(TAssert));

        public static Result Failed<TAssert>(string message) where TAssert : IAssert => new Result(ResultStatus.Failed, message, typeof(TAssert));

        private Result(ResultStatus status, string message, Type assertType)
        {
            Status = status;
            Message = message;
            AssertType = assertType;
        }
    }
}
