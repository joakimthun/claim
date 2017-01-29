using System;
using System.Collections.Generic;
using System.Linq;
using Rigid.Asserts;

namespace Rigid.Exceptions
{
    public class RequestFailedException : Exception
    {
        public RequestFailedException(IEnumerable<Result> failedResults) : base(FormatMessage(failedResults))
        {
        }

        private static string FormatMessage(IEnumerable<Result> failedResults)
        {
            return string.Join(Environment.NewLine, failedResults.Select(x => x.Message));
        }
    }
}
