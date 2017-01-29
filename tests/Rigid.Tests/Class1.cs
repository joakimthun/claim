using System.Net;
using NUnit.Framework;

namespace Rigid.Tests
{
    [TestFixture]
    public class Class1
    {
        [Test]
        public void Test()
        {
            Rigid.Get("https://www.google.com")
                .AssertStatus(HttpStatusCode.BadGateway)
                .Execute();

            Assert.IsFalse(true);
        }
    }
}
