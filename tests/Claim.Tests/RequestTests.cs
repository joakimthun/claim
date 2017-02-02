using System.Collections.Generic;
using System.Linq;
using System.Net;
using NUnit.Framework;
using Claim.Asserts;
using Claim.Exceptions;
using Assert = NUnit.Framework.Assert;

namespace Claim.Tests
{
    [TestFixture]
    public class RequestTests : RequestTestsBase
    {
        [Test]
        public void Assert_status_ok_does_not_throw_an_excepetion_if_the_status_is_ok()
        {
            Claims.Get("https://www.test.com", () => CreateMockedHttpClient(HttpStatusCode.OK))
                .AssertStatus(HttpStatusCode.OK)
                .Execute();
        }

        [Test]
        public void Assert_on_status_throws_an_excepetion_if_the_status_does_not_match()
        {
            var exception = Assert.Catch<AssertFailedException>(() =>
            {
                Claims.Get("https://www.test.com", () => CreateMockedHttpClient(HttpStatusCode.BadGateway))
                    .AssertStatus(HttpStatusCode.OK)
                    .Execute();
            });

            Assert.IsTrue(exception.FailedResults.All(x => x.Status == ResultStatus.Failed && x.AssertType == typeof(StatusCodeAssert)));
        }

        [Test]
        public void Assert_contains_a_single_header_does_not_throw_an_excepetion_if_the_header_is_a_match()
        {
            Claims.Get("https://www.test.com", () => CreateMockedHttpClient(HttpStatusCode.OK, headers: new List<KeyValuePair<string, IEnumerable<string>>> { new KeyValuePair<string, IEnumerable<string>>("my_key", new []{ "my_value" }) }))
                .AssertContainsHeader("my_key", "my_value")
                .Execute();
        }

        [Test]
        public void Assert_contains_a_single_header_throws_an_excepetion_if_the_header_is_not_a_match()
        {
            var exception = Assert.Catch<AssertFailedException>(() =>
            {
                Claims.Get("https://www.test.com", () => CreateMockedHttpClient(HttpStatusCode.OK))
                    .AssertContainsHeader("my_key", "my_value")
                    .Execute();
            });

            Assert.IsTrue(exception.FailedResults.All(x => x.Status == ResultStatus.Failed && x.AssertType == typeof(ContainsHeaderAssert)));
        }

        [Test]
        public void Assert_contains_multiple_headers_does_not_throw_an_excepetion_if_the_headers_are_a_match()
        {
            Claims.Get(
                    "https://www.test.com",
                    () => CreateMockedHttpClient(HttpStatusCode.OK, headers: new List<KeyValuePair<string, IEnumerable<string>>> { new KeyValuePair<string, IEnumerable<string>>("my_key", new[] { "my_value1", "my_value2" }) }))
                .AssertContainsHeader("my_key", new [] { "my_value1", "my_value2" })
                .Execute();
        }

        [Test]
        public void Assert_contains_multiple_headers_throws_an_excepetion_if_the_headers_are_not_a_match()
        {
            var exception = Assert.Catch<AssertFailedException>(() =>
            {
                Claims.Get("https://www.test.com", () => CreateMockedHttpClient(HttpStatusCode.OK))
                    .AssertContainsHeader("my_key", new[] { "my_value1", "my_value2" })
                    .Execute();
            });

            Assert.IsTrue(exception.FailedResults.All(x => x.Status == ResultStatus.Failed && x.AssertType == typeof(ContainsHeaderAssert)));

            exception = Assert.Catch<AssertFailedException>(() =>
            {
                Claims.Get("https://www.test.com", () => CreateMockedHttpClient(HttpStatusCode.OK, headers: new List<KeyValuePair<string, IEnumerable<string>>> { new KeyValuePair<string, IEnumerable<string>>("my_key", new[] { "my_value11", "my_value22" }) }))
                    .AssertContainsHeader("my_key", new[] { "my_value1", "my_value2" })
                    .Execute();
            });

            Assert.IsTrue(exception.FailedResults.All(x => x.Status == ResultStatus.Failed && x.AssertType == typeof(ContainsHeaderAssert)));

            exception = Assert.Catch<AssertFailedException>(() =>
            {
                Claims.Get("https://www.test.com", () => CreateMockedHttpClient(HttpStatusCode.OK, headers: new List<KeyValuePair<string, IEnumerable<string>>> { new KeyValuePair<string, IEnumerable<string>>("my_key", new[] { "my_value1", "my_value2" }) }))
                    .AssertContainsHeader("my_key2", new[] { "my_value1", "my_value2" })
                    .Execute();
            });

            Assert.IsTrue(exception.FailedResults.All(x => x.Status == ResultStatus.Failed && x.AssertType == typeof(ContainsHeaderAssert)));

            exception = Assert.Catch<AssertFailedException>(() =>
            {
                Claims.Get("https://www.test.com", () => CreateMockedHttpClient(HttpStatusCode.OK, headers: new List<KeyValuePair<string, IEnumerable<string>>> { new KeyValuePair<string, IEnumerable<string>>("my_key", new[] { "my_value1" }) }))
                    .AssertContainsHeader("my_key", new[] { "my_value1", "my_value2" })
                    .Execute();
            });

            Assert.IsTrue(exception.FailedResults.All(x => x.Status == ResultStatus.Failed && x.AssertType == typeof(ContainsHeaderAssert)));
        }
    }
}
