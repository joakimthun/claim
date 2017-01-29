using NUnit.Framework;

namespace Rigid.Tests
{
    [TestFixture]
    public class Class1
    {
        [Test]
        public void Test()
        {
            var x = Rigid.Get("");
            Assert.IsFalse(x == null);
        }
    }
}
