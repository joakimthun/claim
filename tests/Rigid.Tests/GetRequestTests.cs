using System.Linq;
using System.Net;
using NUnit.Framework;
using Rigid.Asserts;
using Rigid.Exceptions;
using Assert = NUnit.Framework.Assert;

namespace Rigid.Tests
{
    [TestFixture]
    public class GetRequestTests
    {
        [Test]
        public void Assert_status_ok()
        {
            Rigid.Get("https://www.google.com")
                .AssertStatus(HttpStatusCode.OK)
                .Execute();
        }

        [Test]
        public void Assert_wrong_status_throws_assert_valid_assert_exception()
        {
            var exception = Assert.Catch<AssertFailedException>(() =>
            {
                Rigid.Get("https://www.google.com")
                    .AssertStatus(HttpStatusCode.HttpVersionNotSupported)
                    .Execute();
            });

            Assert.IsTrue(exception.FailedResults.All(x => x.Status == ResultStatus.Failed && x.AssertType == typeof(StatusCodeAssert)));
        }
    }
}
