namespace Rigid.Asserts
{
    public enum ResultStatus : byte
    {
        Passed,
        Failed
    }

    public class Result
    {
        public ResultStatus Status { get; private set; } = ResultStatus.Passed;
        public string Message { get; private set; } = "Passed";

        public static Result Passed() => new Result();

        public static Result Failed(string message)
        {
            return new Result
            {
                Status = ResultStatus.Failed,
                Message = message
            };
        }

        private Result() { }
    }
}
