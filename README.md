# rigid
A tiny framework for testing APIs

### Basic usage
```
  Rigid.Get("https://www.rigid.com/api/v1/info")
    .AssertStatus(HttpStatusCode.OK)
    .AssertContainsHeader("Content-Type", "application/json")
    .AssertJson(new
        {
            type = Rigid.ValueMatchers.String,
            name = "rigid",
            version = Rigid.ValueMatchers.Regex("^1")
        })
    .Execute();
```

### Request method support
Rigid currently supports Get, Post, Put and Delete requests through the 
static Rigid class e.g. Rigid.Get(...).

### Asserts
##### AssertStatus
Asserts that the actual status code equals the expected one.

Example:
```
  Rigid.Get("https://www.rigid.com/api/v1/info")
    .AssertStatus(HttpStatusCode.OK)
    .Execute();
```
##### AssertContainsHeader
Asserts that http response contains the specified header and value.

Example:
```
  Rigid.Get("https://www.rigid.com/api/v1/info")
    .AssertContainsHeader("Content-Type", "application/json")
    .Execute();
```

##### AssertJson
Asserts that http response matches the expected json structure and/or values and types.

Example:
```
  Rigid.Get("https://www.rigid.com/api/v1/info")
    .AssertJson(new
      {
        // Macthes any string
        type = Rigid.ValueMatchers.String,
        // Matches the exact string "rigid"
        name = "rigid",
        // Matches anything that starts with "1"
        version = Rigid.ValueMatchers.Regex("^1")
      })
    .Execute();
```

