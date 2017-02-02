using System.Linq;
using NUnit.Framework;
using Claim.Asserts;
using Claim.Exceptions;
using Assert = NUnit.Framework.Assert;

namespace Claim.Tests
{
    [TestFixture]
    public class JsonTests : RequestTestsBase
    {
        [Test]
        public void Assert_json_does_not_throw_an_exception_for_a_property_and_value_match()
        {
            Claim.Get("https://www.test.com", () => CreateMockedJsonHttpClient("{ \"Test\": 123 }"))
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
                Claim.Get("https://www.test.com", () => CreateMockedJsonHttpClient("{ \"Test\": 321 }"))
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
                Claim.Get("https://www.test.com", () => CreateMockedJsonHttpClient("{ \"myProperty\": 321 }"))
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
                Claim.Get("https://www.test.com", () => CreateMockedJsonHttpClient("{ \"myProperty\": 321 }"))
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
            Claim.Get("https://www.test.com", () => CreateMockedJsonHttpClient("{ \"myProperty\": 123 }"))
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

            Claim.Get("https://www.test.com", () => CreateMockedJsonHttpClient(actual))
                .AssertJson(actual)
                .Execute();
        }

        [Test]
        public void Assert_json_throws_a_correct_exception_when_multiple_properties_of_different_types_are_not_present_in_the_response()
        {
            var exception = Assert.Catch<AssertFailedException>(() =>
            {
                Claim.Get("https://www.test.com", () => CreateMockedJsonHttpClient(new {Test = 777}))
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

        [Test]
        public void Assert_json_handles_multiple_levels_of_nested_objects_correctly()
        {
            var actual = new
            {
                MyInt = 123,
                MyDouble = 123.123,
                MyString = "Hello!!!!????",
                MyFirstNestedObject = new
                {
                    MyInt = 123,
                    MyDouble = 123.123,
                    MyString = "Hello!!!!????",
                    MySecondNestedObject = new
                    {
                        MyInt = 123,
                        MyDouble = 123.123,
                        MyString = "Hello!!!!????"
                    }
                }
            };

            Claim.Get("https://www.test.com", () => CreateMockedJsonHttpClient(actual))
                .AssertJson(actual)
                .Execute();
        }

        [Test]
        public void Assert_json_correctly_generates_multiple_level_nested_error_messages()
        {
            var actual = new
            {
                MyInt = 123,
                MyDouble = 123.123,
                MyString = "Hello!!!!????",
                MyFirstNestedObject = new
                {
                    MyInt = 123,
                    MyDouble = 123.123,
                    MyString = "Hello!!!!????",
                    MySecondNestedObject = new
                    {
                        MyInt = 123,
                        MyDouble = 123.123,
                        MyString = "Hello!!!!????"
                    }
                }
            };

            var exception = Assert.Catch<AssertFailedException>(() =>
            {
                Claim.Get("https://www.test.com", () => CreateMockedJsonHttpClient(actual))
                    .AssertJson(new
                    {
                        MyInt = 1234,
                        MyDouble = 123.123,
                        MyString = "Hello!!!!????",
                        MyFirstNestedObject = new
                        {
                            MyInt = 123,
                            MyDouble = 123.123,
                            MyString = "World...",
                            MySecondNestedObject = new
                            {
                                MyInt = 1234,
                                MyDouble = 123.123,
                                MyString = ":("
                            }
                        }
                    })
                    .Execute();
            });

            Assert.IsTrue(exception.FailedResults.Single().Message.Contains("The expected property 'MyInt' does not have the same value as the property in the response. Expected value: '1234'. Actual value: '123'"));
            Assert.IsTrue(exception.FailedResults.Single().Message.Contains("The expected property 'MyFirstNestedObject.MyString' does not have the same value as the property in the response. Expected value: 'World...'. Actual value: 'Hello!!!!????'"));
            Assert.IsTrue(exception.FailedResults.Single().Message.Contains("The expected property 'MyFirstNestedObject.MySecondNestedObject.MyInt' does not have the same value as the property in the response. Expected value: '1234'. Actual value: '123'"));
            Assert.IsTrue(exception.FailedResults.Single().Message.Contains("The expected property 'MyFirstNestedObject.MySecondNestedObject.MyString' does not have the same value as the property in the response. Expected value: ':('. Actual value: 'Hello!!!!????'"));
        }

        [Test]
        public void Assert_json_correctly_handles_arrays()
        {
            var actual = new
            {
                MyInts = new [] { 1, 2, 4 }
            };

            Claim.Get("https://www.test.com", () => CreateMockedJsonHttpClient(actual))
                .AssertJson(new
                {
                    MyInts = new[] { 1, 2, 4 }
                })
                .Execute();
        }

        [Test]
        public void Assert_json_correctly_generates_a_length_mismatch_error_for_arrays_of_different_lenghts()
        {
            var actual = new
            {
                MyInts = new[] { 1, 2, 4 }
            };

            var exception = Assert.Catch<AssertFailedException>(() =>
            {
                Claim.Get("https://www.test.com", () => CreateMockedJsonHttpClient(actual))
                    .AssertJson(new
                    {
                        MyInts = new[] {1, 2, 4, 5 }
                    })
                    .Execute();
            });

            Assert.IsTrue(exception.FailedResults.Single().Message.Contains("The expected array property 'MyInts' is not of the same length as the array in the response. Expected length: '4'. Actual length: '3'"));
        }

        [Test]
        public void Assert_json_correctly_generates_a_type_mismatch_error_for_arrays_of_different_types()
        {
            var actual = new
            {
                MyInts = new[] { 1, 2, 4 }
            };

            var exception = Assert.Catch<AssertFailedException>(() =>
            {
                Claim.Get("https://www.test.com", () => CreateMockedJsonHttpClient(actual))
                    .AssertJson(new
                    {
                        MyInts = new[] { "1", "2", "4" }
                    })
                    .Execute();
            });

            Assert.IsTrue(exception.FailedResults.Single().Message.Contains("The expected property 'MyInts[0]' is not of the same type as the property in the response. Expected type: 'String'. Actual type: 'Integer'"));
            Assert.IsTrue(exception.FailedResults.Single().Message.Contains("The expected property 'MyInts[1]' is not of the same type as the property in the response. Expected type: 'String'. Actual type: 'Integer'"));
            Assert.IsTrue(exception.FailedResults.Single().Message.Contains("The expected property 'MyInts[2]' is not of the same type as the property in the response. Expected type: 'String'. Actual type: 'Integer'"));
        }

        [Test]
        public void Assert_json_correctly_handles_object_arrays()
        {
            var actual = new
            {
                MyObjects = new[]
                {
                   new { MyFirstArrayObjectProp = 1, MySecondArrayObjectProp = 2 }
                }
            };

            Claim.Get("https://www.test.com", () => CreateMockedJsonHttpClient(actual))
                .AssertJson(actual)
                .Execute();
        }

        [Test]
        public void Assert_json_correctly_generates_errors_for_arrays_of_objects_with_properties_of_different_types_or_names()
        {
            var actual = new
            {
                MyObjects = new[]
                {
                   new { MyFirstArrayObjectProp = 1, MySecondArrayObjectProp = 2.0, MyThirdArrayObjectProp = "Hello!" }
                }
            };

            var exception = Assert.Catch<AssertFailedException>(() =>
            {
                Claim.Get("https://www.test.com", () => CreateMockedJsonHttpClient(actual))
                    .AssertJson(new
                    {
                        MyObjects = new[]
                        {
                            new { MyFirstArrayObjectProp = 1.0, MySecondArrayObjectProp1 = 2.0, MyThirdArrayObjectProp = 1 }
                        }
                    })
                    .Execute();
            });

            Assert.IsTrue(exception.FailedResults.Single().Message.Contains("The expected property 'MyObjects[0].MyFirstArrayObjectProp' is not of the same type as the property in the response. Expected type: 'Double'. Actual type: 'Integer'"));
            Assert.IsTrue(exception.FailedResults.Single().Message.Contains("The expected property 'MyObjects[0].MySecondArrayObjectProp1' was not present in the response."));
            Assert.IsTrue(exception.FailedResults.Single().Message.Contains("The expected property 'MyObjects[0].MyThirdArrayObjectProp' is not of the same type as the property in the response. Expected type: 'Int32'. Actual type: 'String'"));
        }

        [Test]
        public void Assert_json_correctly_handles_nested_object_arrays()
        {
            var actual = new
            {
                MyObjects = new[]
                {
                    new
                    {
                        MyFirstArrayObjectProp = 1,
                        MySecondArrayObjectProp = 2.0,
                        MyNestedObjects = new[] 
                            {
                                new
                                {
                                    MyFirstArrayObjectProp = 1,
                                    MySecondArrayObjectProp = 2,
                                    MySecondLevelNestedObjects = new [] { "123" }
                                },
                                new
                                {
                                    MyFirstArrayObjectProp = 1,
                                    MySecondArrayObjectProp = 2,
                                    MySecondLevelNestedObjects = new [] { "123" }
                                },
                                new
                                {
                                    MyFirstArrayObjectProp = 1,
                                    MySecondArrayObjectProp = 2,
                                    MySecondLevelNestedObjects = new [] { "123" }
                                }
                            }
                    }
                }
            };

            Claim.Get("https://www.test.com", () => CreateMockedJsonHttpClient(actual))
                .AssertJson(actual)
                .Execute();
        }

        [Test]
        public void Assert_json_correctly_generates_errors_for_nested_arrays_of_objects_with_properties_of_different_types_or_names()
        {
            var actual = new
            {
                MyObjects = new[]
                {
                    new
                    {
                        MyFirstArrayObjectProp = 1,
                        MySecondArrayObjectProp = 2.0,
                        MyNestedObjects = new[]
                            {
                                new
                                {
                                    MyFirstArrayObjectProp = 1,
                                    MySecondArrayObjectProp = 2,
                                    MySecondLevelNestedObjects = new [] { "123" },
                                    MyWrongValueProperty = 12
                                },
                                new
                                {
                                    MyFirstArrayObjectProp = 1,
                                    MySecondArrayObjectProp = 2,
                                    MySecondLevelNestedObjects = new [] { "123" },
                                    MyWrongValueProperty = 12
                                },
                                new
                                {
                                    MyFirstArrayObjectProp = 1,
                                    MySecondArrayObjectProp = 2,
                                    MySecondLevelNestedObjects = new [] { "123" },
                                    MyWrongValueProperty = 12
                                }
                            }
                    }
                }
            };

            var exception = Assert.Catch<AssertFailedException>(() =>
            {
                Claim.Get("https://www.test.com", () => CreateMockedJsonHttpClient(actual))
                    .AssertJson(new
                    {
                        MyObjects = new[]
                        {
                            new
                            {
                                MyFirstArrayObjectProp = "1", // Wrong Type
                                MySecondArrayObjectProp = 2, // Wrong Type
                                MyNestedObjects = new[]
                                {
                                    new
                                    {
                                        MyFirstArrayObjectProp1 = 1, // Wrong Name
                                        MySecondArrayObjectProp = 2.0, // Wrong Type
                                        MySecondLevelNestedObjects = new[] {123}, // Wrong Type
                                        MyWrongValueProperty = 13 // Wrong Value
                                    },
                                    new
                                    {
                                        MyFirstArrayObjectProp1 = 1, // Wrong Name
                                        MySecondArrayObjectProp = 2.0, // Wrong Type
                                        MySecondLevelNestedObjects = new[] {123}, // Wrong Type
                                        MyWrongValueProperty = 13 // Wrong Value
                                    },
                                    new
                                    {
                                        MyFirstArrayObjectProp1 = 1, // Wrong Name
                                        MySecondArrayObjectProp = 2.0, // Wrong Type
                                        MySecondLevelNestedObjects = new[] {123}, // Wrong Type
                                        MyWrongValueProperty = 13 // Wrong Value
                                    }
                                }
                            }
                        }
                    })
                    .Execute();
            });

            Assert.IsTrue(exception.FailedResults.Single().Message.Contains("The expected property 'MyObjects[0].MyFirstArrayObjectProp' is not of the same type as the property in the response. Expected type: 'String'. Actual type: 'Integer'"));
            Assert.IsTrue(exception.FailedResults.Single().Message.Contains("The expected property 'MyObjects[0].MySecondArrayObjectProp' is not of the same type as the property in the response. Expected type: 'Int32'. Actual type: 'Float'"));
            Assert.IsTrue(exception.FailedResults.Single().Message.Contains("The expected property 'MyObjects[0].MyNestedObjects[0].MyFirstArrayObjectProp1' was not present in the response."));
            Assert.IsTrue(exception.FailedResults.Single().Message.Contains("The expected property 'MyObjects[0].MyNestedObjects[0].MySecondArrayObjectProp' is not of the same type as the property in the response. Expected type: 'Double'. Actual type: 'Integer'"));
            Assert.IsTrue(exception.FailedResults.Single().Message.Contains("The expected property 'MyObjects[0].MyNestedObjects[0].MySecondLevelNestedObjects[0]' is not of the same type as the property in the response. Expected type: 'Int32'. Actual type: 'String'"));
            Assert.IsTrue(exception.FailedResults.Single().Message.Contains("The expected property 'MyObjects[0].MyNestedObjects[0].MyWrongValueProperty' does not have the same value as the property in the response. Expected value: '13'. Actual value: '12'"));
            Assert.IsTrue(exception.FailedResults.Single().Message.Contains("The expected property 'MyObjects[0].MyNestedObjects[1].MyFirstArrayObjectProp1' was not present in the response."));
            Assert.IsTrue(exception.FailedResults.Single().Message.Contains("The expected property 'MyObjects[0].MyNestedObjects[1].MySecondArrayObjectProp' is not of the same type as the property in the response. Expected type: 'Double'. Actual type: 'Integer'"));
            Assert.IsTrue(exception.FailedResults.Single().Message.Contains("The expected property 'MyObjects[0].MyNestedObjects[1].MySecondLevelNestedObjects[0]' is not of the same type as the property in the response. Expected type: 'Int32'. Actual type: 'String'"));
            Assert.IsTrue(exception.FailedResults.Single().Message.Contains("The expected property 'MyObjects[0].MyNestedObjects[1].MyWrongValueProperty' does not have the same value as the property in the response. Expected value: '13'. Actual value: '12'"));
            Assert.IsTrue(exception.FailedResults.Single().Message.Contains("The expected property 'MyObjects[0].MyNestedObjects[2].MyFirstArrayObjectProp1' was not present in the response."));
            Assert.IsTrue(exception.FailedResults.Single().Message.Contains("The expected property 'MyObjects[0].MyNestedObjects[2].MySecondArrayObjectProp' is not of the same type as the property in the response. Expected type: 'Double'. Actual type: 'Integer'"));
            Assert.IsTrue(exception.FailedResults.Single().Message.Contains("The expected property 'MyObjects[0].MyNestedObjects[2].MySecondLevelNestedObjects[0]' is not of the same type as the property in the response. Expected type: 'Int32'. Actual type: 'String'"));
            Assert.IsTrue(exception.FailedResults.Single().Message.Contains("The expected property 'MyObjects[0].MyNestedObjects[2].MyWrongValueProperty' does not have the same value as the property in the response. Expected value: '13'. Actual value: '12'"));
        }
    }
}
