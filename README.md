# claim
A tiny framework for testing APIs

## Basic usage with [NUnit](https://github.com/nunit/nunit)
```
  [Test]
  public void MyTest()
  {
    Claims.Get("https://www.claim.com/api/v1/test")
      .AssertStatus(HttpStatusCode.OK)
      .AssertContainsHeader("Content-Type", "application/json")
      .AssertJson(new
      {
        type = Matchers.String,
        name = "claim",
        version = Matchers.Regex("^1")
      })
      .Execute();
  }
```

## Requests
Claim currently supports Get, Post, Put and Delete requests through the 
static Claims class.

Examples:
```
  Claims.Get("https://www.claim.com/api/v1/test").Execute();
  
  // Json post
  Claims.Post("https://www.claim.com/api/v1/test", new { prop = 123 }).Execute();
```

If you need to add an Authorization header or do other things like modifing the request before is is sent
by inheriting from the HttpClient class and overriding SendAsync you can provide your custom HttpClient for 
all request types.

Example:
```
  Claims.Get("https://www.claim.com/api/v1/test", () => new MyHttpClient())
      .Execute();
```

## Asserts
#### AssertStatus
Asserts that the actual status code equals the expected one.

Example:
```
  Claims.Get("https://www.claim.com/api/v1/test")
    .AssertStatus(HttpStatusCode.OK)
    .Execute();
```
#### AssertContainsHeader
Asserts that the http response contains the specified header and value.

Example:
```
  Claims.Get("https://www.claim.com/api/v1/test")
    .AssertContainsHeader("Content-Type", "application/json")
    .Execute();
```

#### AssertJson
Asserts that the http response matches the expected json structure and/or values and types.

Matching exact values:
```
  Claims.Get("https://www.claim.com/api/v1/test")
    .AssertJson(new
      {
        name = "claim",
        age = 99
      })
    .Execute();
```

Claim also supports matching properties on just the property type or by using a regex:
```
  Claims.Get("https://www.claim.com/api/v1/test")
    .AssertJson(new
      {
        myStringProperty = Matchers.String,
        myIntProperty = Matchers.Int,
        myFloatProperty = Matchers.Float,
        myRegexProperty = Matchers.Regex("^He"),
        myArrayProperty = Matchers.Array,
        myObjectProperty = Matchers.Object,
        myBooleanProperty = Matchers.Boolean,
        myNullProperty = Matchers.Null,
        myDate1 = Matchers.Date,
        myDate2 = Matchers.MatchDate("2017-01-25T14:30Z", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind)
      })
    .Execute();
```

You can also write custom property matchers by implementing IPropertyValueMatcher:
```
    public class DaylightSavingTimeMacther : IPropertyValueMatcher
    {
      public MatchingResult Match(PropertyInfo expectedProperty, JToken actualValue)
      {
        // Probably want to catch an InvalidCastException here
        var success = ((DateTime)actualValue).IsDaylightSavingTime();

        return new MatchingResult
        {
          Success = success,
          // Claim will take care of assigning the error to the correct property
          Message = success ? string.Empty : $"Not daylight saving time :("
         };
      }
    }

    Claims.Get("https://www.claim.com/api/v1/test")
      .AssertJson(new
      {
        myProperty = new DaylightSavingTimeMacther(),
      })
      .Execute();
```

#### Custom asserts
Claim supports writing your custom asserts by implementing IAssert:
```
    public class HttpVersionAssert : IAssert
    {
      public Result Assert(Response response)
      {
        if (response.ResponseMessage.Version != Version.Parse("1.1"))
          return Result.Failed<HttpVersionAssert>($"Expected http version '1.1' but got '{response.ResponseMessage.Version}'.");

        return Result.Passed<HttpVersionAssert>();
      }
    }

    Claims.Get("https://www.claim.com/api/v1/test")
      .AddAssert(new HttpVersionAssert())
      .Execute();
```

