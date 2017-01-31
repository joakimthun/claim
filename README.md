# rigid
A tiny framework for testing APIs

######Basic usage
```
  Rigid.Get("https://www.test.com")
    .AssertStatus(HttpStatusCode.OK)
    .AssertContainsHeader("my_key", "my_value")
    .AssertJson(new
        {
            intValue = 123,
            floatValue = 1.23,
            stringValue = "Hello World!"
        })
    .Execute();
```
