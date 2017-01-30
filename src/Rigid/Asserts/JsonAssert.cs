using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Rigid.Extensions;

namespace Rigid.Asserts
{
    public enum PropertyComparison : byte
    {
        IgnoreCase
    }

    public enum ExpectedProperyValue : byte
    {
        Any,
        Null,
        String,
        Int,
        Bool,
        Object,
        Array,
        Float,
        Date
    }

    public class JsonAssert : Assert
    {
        private readonly object _expectedResponseStructure;
        private readonly PropertyComparison? _propertyComparison;
        private readonly ICollection<string> _errors = new List<string>();
        private readonly Stack<string> _expectedPropertyPath = new Stack<string>();

        public JsonAssert(object expectedResponseStructure, PropertyComparison? propertyComparison = null)
        {
            _expectedResponseStructure = expectedResponseStructure;
            _propertyComparison = propertyComparison;
        }

        public JsonAssert(string expectedResponseStructure, PropertyComparison? propertyComparison = null)
        {
            _expectedResponseStructure = JsonConvert.DeserializeObject(expectedResponseStructure);
            _propertyComparison = propertyComparison;
        }

        public override Result Execute(Response response)
        {
            Verify(_expectedResponseStructure, JObject.Parse(Encoding.UTF8.GetString(response.ResponseContent)));

            if (_errors.Any())
                return Failed<JsonAssert>(_errors.ToNewLineSeparatedList());

            return Passed<JsonAssert>();
        }

        private void Verify(object expected, JObject actual, string parentExpectedProperty = null)
        {
            if (parentExpectedProperty != null)
            {
                _expectedPropertyPath.Push(parentExpectedProperty);
            }

            foreach (var property in expected.GetProperties())
            {
                VerifyProperty(property, expected, actual);
            }

            if (parentExpectedProperty != null)
            {
                _expectedPropertyPath.Pop();
            }
        }

        private void VerifyProperty(PropertyInfo expectedProperty, object expected, JObject actual)
        {
            JProperty actualProperty;
            if (_propertyComparison == PropertyComparison.IgnoreCase)
            {
                actualProperty = actual.Properties().SingleOrDefault(x => string.Equals(x.Name, expectedProperty.Name, StringComparison.OrdinalIgnoreCase));
            }
            else
            {
                actualProperty = actual.Properties().SingleOrDefault(x => x.Name == expectedProperty.Name);
            }

            if (actualProperty == null)
            {
                _errors.Add($"The expected property '{GetExpectedPropertyPathName(expectedProperty)}' was not present in the response.");
                return;
            }

            var expectedPropertyValue = expectedProperty.GetValue(expected);
            VerifyProperty(expectedProperty, expectedPropertyValue, actualProperty.Value);
        }

        private void VerifyProperty(PropertyInfo expectedProperty, object expected, JToken actual)
        {
            if (VerifyExpectedProperyValue(expected, actual))
                return;

            CompareTypeAndValue(expectedProperty, expected, actual);
        }

        private void CompareTypeAndValue(PropertyInfo expectedProperty, object expected, JToken actual)
        {
            if (actual.Type == JTokenType.Object)
            {
                Verify(expected, (JObject)actual, expectedProperty.Name);
                return;
            }

            try
            {
                var actualValue = actual.CastToSystemType(expected.GetType());

                if (!expected.Equals(actualValue))
                    _errors.Add($"The expected property '{GetExpectedPropertyPathName(expectedProperty)}' does not have the same value as the property in the response. Expected value: '{expected}'. Actual value: '{actualValue}'");
            }
            catch (InvalidCastException)
            {
                var expectedPropertyTypeName = expectedProperty.PropertyType.Name.Contains("AnonymousType") ? "Object" : expectedProperty.PropertyType.Name;
                _errors.Add($"The expected property '{GetExpectedPropertyPathName(expectedProperty)}' is not of the same type as the property in the response. Expected type: '{expectedPropertyTypeName}'. Actual type: '{actual.Type}'");
            }
        }

        private string GetExpectedPropertyPathName(PropertyInfo currentLevelExpectedProperty)
        {
            if (!_expectedPropertyPath.Any())
                return currentLevelExpectedProperty.Name;

            return string.Join(".", _expectedPropertyPath.Reverse()) + "." + currentLevelExpectedProperty.Name;
        }

        private static bool VerifyExpectedProperyValue(object expectedValue, JToken actualValue)
        {
            if (!(expectedValue is ExpectedProperyValue))
                return false;

            switch ((ExpectedProperyValue)expectedValue)
            {
                case ExpectedProperyValue.Any:
                    return true;
                case ExpectedProperyValue.Null:
                    return actualValue.Type == JTokenType.Null;
                case ExpectedProperyValue.String:
                    return actualValue.Type == JTokenType.String;
                case ExpectedProperyValue.Int:
                    return actualValue.Type == JTokenType.Integer;
                case ExpectedProperyValue.Bool:
                    return actualValue.Type == JTokenType.Boolean;
                case ExpectedProperyValue.Object:
                    return actualValue.Type == JTokenType.Object;
                case ExpectedProperyValue.Array:
                    return actualValue.Type == JTokenType.Array;
                case ExpectedProperyValue.Float:
                    return actualValue.Type == JTokenType.Float;
                case ExpectedProperyValue.Date:
                    return actualValue.Type == JTokenType.Date;
                default:
                    throw new ArgumentOutOfRangeException(nameof(expectedValue), expectedValue, $"The ExpectedProperyValue: '{expectedValue}' is not yet supported.");
            }
        }
    }
}
