using System;
using System.Collections.Generic;
using System.Linq;
using Rigid.Asserts;

namespace Rigid.Exceptions
{
    public class AssertFailedException : Exception
    {
        public AssertFailedException(IEnumerable<Result> failedResults) : base(FormatMessage(failedResults)) {}

        private static string FormatMessage(IEnumerable<Result> failedResults) => string.Join(Environment.NewLine, failedResults.Select(x => x.Message));
    }
}
