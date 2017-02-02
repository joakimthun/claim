using System;
using System.Collections.Generic;
using System.Linq;
using Claim.Extensions;

namespace Claim.Asserts
{
    public class ContainsHeaderAssert : IAssert
    {
        private readonly string _name;
        private readonly IEnumerable<string> _values;

        public ContainsHeaderAssert(string name, IEnumerable<string> values)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentException(nameof(name));
            if (values == null) throw new ArgumentNullException(nameof(values));

            _name = name;
            _values = values;
        }

        public Result Assert(Response response)
        {
            IEnumerable<string> actualValues;
            var success = response.ResponseMessage.Headers.TryGetValues(_name, out actualValues);
            if (!success)
                success = response.ResponseMessage?.Content?.Headers?.TryGetValues(_name, out actualValues) ?? false;

            if (success)
            {
                var failed = Result.Failed<ContainsHeaderAssert>($"The actual header values does not match the expected header values. Expected: '{_values.ToCommaSeparatedList()}'. Actual: '{actualValues.ToCommaSeparatedList()}'.");

                if (_values.Count() != actualValues.Count())
                    return failed;

                foreach (var expectedValue in _values)
                {
                    var match = actualValues.SingleOrDefault(x => x == expectedValue);
                    if (match == null)
                        return failed;
                }

                return Result.Passed<ContainsHeaderAssert>();
            }

            return Result.Failed<ContainsHeaderAssert>($"The expected header '{_name}' was not present in the response.");
        }
    }
}
