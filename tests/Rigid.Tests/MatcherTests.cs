using System.Linq;
using NUnit.Framework;
using Rigid.Exceptions;

namespace Rigid.Tests
{
    [TestFixture]
    public class MatcherTests : RequestTestsBase
    {
        [Test]
        public void Type_matchers_matches_the_property_if_match_returns_true()
        {
            Rigid.Get("https://www.test.com", () => CreateMockedJsonHttpClient(new
                {
                    TestStr = "123",
                    TestInt = 123,
                    TestFloat = 1.23f,
                    TestRegex = "Hello!",
                    TestArray = new [] { 1 ,2, 3 },
                    TestObject = new { Prop = 123 }
                }))
                .AssertJson(new
                    {
                        TestStr = Rigid.ValueMatchers.String,
                        TestInt = Rigid.ValueMatchers.Int,
                        TestFloat = Rigid.ValueMatchers.Float,
                        TestRegex = Rigid.ValueMatchers.Regex("^He"),
                        TestArray = Rigid.ValueMatchers.Array,
                        TestObject = Rigid.ValueMatchers.Object
                    })
                .Execute();
        }

        [Test]
        public void Type_matchers_generates_a_correct_error_message_if_match_returns_false()
        {
            var exception = Assert.Catch<AssertFailedException>(() =>
            {
                Rigid.Get("https://www.test.com", () => CreateMockedJsonHttpClient(new
                {
                    TestStr = "123",
                    TestInt = 123,
                    TestFloat = 1.23f,
                    TestRegex = "Hello!",
                    TestArray = new[] { 1, 2, 3 },
                    TestObject = new { Prop = 123 }
                }))
                .AssertJson(new
                {
                    TestStr = Rigid.ValueMatchers.Int,
                    TestInt = Rigid.ValueMatchers.String,
                    TestFloat = Rigid.ValueMatchers.Regex("^He"),
                    TestRegex = Rigid.ValueMatchers.Float,
                    TestArray = Rigid.ValueMatchers.Object,
                    TestObject = Rigid.ValueMatchers.Array
                })
                .Execute();
            });

            Assert.IsTrue(exception.FailedResults.Single().Message.Contains("The property 'TestStr' did not match the specified matcher. Message: Type mismatch. Expected: 'Integer', Actual: 'String'."));
            Assert.IsTrue(exception.FailedResults.Single().Message.Contains("The property 'TestInt' did not match the specified matcher. Message: Type mismatch. Expected: 'String', Actual: 'Integer'."));
            Assert.IsTrue(exception.FailedResults.Single().Message.Contains("The property 'TestFloat' did not match the specified matcher. Message: The RegexMatcher did not match the actual value: '1.23'."));
            Assert.IsTrue(exception.FailedResults.Single().Message.Contains("The property 'TestRegex' did not match the specified matcher. Message: Type mismatch. Expected: 'Float', Actual: 'String'."));
            Assert.IsTrue(exception.FailedResults.Single().Message.Contains("The property 'TestArray' did not match the specified matcher. Message: Type mismatch. Expected: 'Object', Actual: 'Array'."));
            Assert.IsTrue(exception.FailedResults.Single().Message.Contains("The property 'TestObject' did not match the specified matcher. Message: Type mismatch. Expected: 'Array', Actual: 'Object'."));
        }

        [Test]
        public void Configurable_array_matcher_handles_variable_length_arrays_correctly()
        {
            Rigid.Get("https://www.test.com", () => CreateMockedJsonHttpClient(new
            {
                IntArray = new[] { 1, 2, 3 },
                ObjectArray = new[]
                {
                    new { StrProperty = "123" },
                    new { StrProperty = "456" },
                    new { StrProperty = "789" },
                    new { StrProperty = "999" }
                }

            }))
            .AssertJson(new
            {
                IntArray = Rigid.ValueMatchers.ConfigurableJsonArrayMatcher(new []{ 1, 2 }, false),
                ObjectArray = Rigid.ValueMatchers.ConfigurableJsonArrayMatcher(new[]
                {
                    new { StrProperty = Rigid.ValueMatchers.String },
                    new { StrProperty = Rigid.ValueMatchers.String },
                    new { StrProperty = Rigid.ValueMatchers.String }
                }, false)
            })
            .Execute();
        }

        [Test]
        public void Configurable_array_matcher_creates_error_messages_correctly()
        {
            var exception = Assert.Catch<AssertFailedException>(() =>
            {
                Rigid.Get("https://www.test.com", () => CreateMockedJsonHttpClient(new
                    {
                        IntArray = new[] {1, 2, 3},
                        ObjectArray = new[]
                        {
                            new {StrProperty = "123"},
                            new {StrProperty = "456"},
                            new {StrProperty = "789"},
                            new {StrProperty = "999"}
                        }

                    }))
                    .AssertJson(new
                    {
                        IntArray = Rigid.ValueMatchers.ConfigurableJsonArrayMatcher(new[] {1, 2}, true),
                        ObjectArray = Rigid.ValueMatchers.ConfigurableJsonArrayMatcher(new[]
                        {
                            new {StrProperty = Rigid.ValueMatchers.Int},
                            new {StrProperty = Rigid.ValueMatchers.Int},
                            new {StrProperty = Rigid.ValueMatchers.Int}
                        }, false)
                    })
                    .Execute();
            });

            Assert.IsTrue(exception.FailedResults.Single().Message.Contains("The property 'IntArray' did not match the specified matcher. Message: The expected array property 'IntArray' is not of the same length as the array in the response. Expected length: '2'. Actual length: '3'"));
            Assert.IsTrue(exception.FailedResults.Single().Message.Contains("The property 'ObjectArray' did not match the specified matcher. Message: The property 'ObjectArray[0].StrProperty' did not match the specified matcher. Message: Type mismatch. Expected: 'Integer', Actual: 'String'."));
            Assert.IsTrue(exception.FailedResults.Single().Message.Contains("The property 'ObjectArray' did not match the specified matcher. Message: The property 'ObjectArray[1].StrProperty' did not match the specified matcher. Message: Type mismatch. Expected: 'Integer', Actual: 'String'."));
            Assert.IsTrue(exception.FailedResults.Single().Message.Contains("The property 'ObjectArray' did not match the specified matcher. Message: The property 'ObjectArray[2].StrProperty' did not match the specified matcher. Message: Type mismatch. Expected: 'Integer', Actual: 'String'."));
        }
    }
}
