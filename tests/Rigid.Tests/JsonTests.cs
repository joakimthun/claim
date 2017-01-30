using System.Linq;
using NUnit.Framework;
using Rigid.Asserts;
using Rigid.Exceptions;
using Assert = NUnit.Framework.Assert;

namespace Rigid.Tests
{
    [TestFixture]
    public class JsonTests : RequestTestsBase
    {
        [Test]
        public void Assert_json_does_not_throw_an_exception_for_a_property_and_value_match()
        {
            Rigid.Get("https://www.google.com", () => CreateMockedJsonHttpClient("{ \"Test\": 123 }"))
                .AssertJson(new
                    {
                        Test = 123
                    })
                .Execute();
        }

        [Test]
        public void Assert_json_throws_a_correct_exception_for_a_value_mismatch()
        {
            var exception = Assert.Catch<AssertFailedException>(() =>
            {
                Rigid.Get("https://www.google.com", () => CreateMockedJsonHttpClient("{ \"Test\": 321 }"))
                    .AssertJson(new
                    {
                        Test = 123
                    })
                    .Execute();
            });

            Assert.AreEqual("The expected property 'Test' does not have the same value as the property in the response. Expected value: '123'. Actual value: '321'", exception.FailedResults.Single().Message);
        }

        [Test]
        public void Assert_json_throws_a_correct_exception_for_a_property_name_mismatch()
        {
            var exception = Assert.Catch<AssertFailedException>(() =>
            {
                Rigid.Get("https://www.google.com", () => CreateMockedJsonHttpClient("{ \"myProperty\": 321 }"))
                    .AssertJson(new
                    {
                        Test = 123
                    })
                    .Execute();
            });

            Assert.AreEqual("The expected property 'Test' was not present in the response.", exception.FailedResults.Single().Message);
        }

        [Test]
        public void Assert_json_throws_a_correct_exception_for_a_property_name_case_mismatch()
        {
            var exception = Assert.Catch<AssertFailedException>(() =>
            {
                Rigid.Get("https://www.google.com", () => CreateMockedJsonHttpClient("{ \"myProperty\": 321 }"))
                    .AssertJson(new
                    {
                        MyProperty = 123
                    })
                    .Execute();
            });

            Assert.AreEqual("The expected property 'MyProperty' was not present in the response.", exception.FailedResults.Single().Message);
        }

        [Test]
        public void Assert_json_does_not_throw_an_exception_for_a_property_name_case_mismatch_if_ignore_case_is_specified()
        {
            Rigid.Get("https://www.google.com", () => CreateMockedJsonHttpClient("{ \"myProperty\": 123 }"))
                .AssertJson(new
                {
                    MyProperty = 123
                }, PropertyComparison.IgnoreCase)
                .Execute();
        }

        [Test]
        public void Assert_json_handles_multiple_properties_of_different_types_correctly()
        {
            var actual = new
            {
                MyInt = 123,
                MyDouble = 123.123,
                MyString = "Hello!!!!????"
            };

            Rigid.Get("https://www.google.com", () => CreateMockedJsonHttpClient(actual))
                .AssertJson(actual)
                .Execute();
        }

        [Test]
        public void Assert_json_throws_a_correct_exception_when_multiple_properties_of_different_types_are_not_present_in_the_response()
        {
            var exception = Assert.Catch<AssertFailedException>(() =>
            {
                Rigid.Get("https://www.google.com", () => CreateMockedJsonHttpClient(new {Test = 777}))
                    .AssertJson(new
                    {
                        MyInt = 123,
                        MyDouble = 123.123,
                        MyString = "Hello!!!!????"
                    })
                    .Execute();
            });

            Assert.IsTrue(exception.FailedResults.Single().Message.Contains("The expected property 'MyInt' was not present in the response."));
            Assert.IsTrue(exception.FailedResults.Single().Message.Contains("The expected property 'MyDouble' was not present in the response."));
            Assert.IsTrue(exception.FailedResults.Single().Message.Contains("The expected property 'MyString' was not present in the response."));
        }

        //[Test]
        //public void Assert_on_status_throws_an_excepetion_if_the_status_does_not_match()
        //{
        //    var exception = Assert.Catch<AssertFailedException>(() =>
        //    {
        //        Rigid.Get("https://www.google.com", () => CreateMockedHttpClient(HttpStatusCode.BadGateway))
        //            .AssertStatus(HttpStatusCode.OK)
        //            .Execute();
        //    });

        //    Assert.IsTrue(exception.FailedResults.All(x => x.Status == ResultStatus.Failed && x.AssertType == typeof(StatusCodeAssert)));
        //}
    }
}
