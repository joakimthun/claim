# claim
A tiny framework for testing APIs

### Basic usage
```
  Claims.Get("https://www.claim.com/api/v1/info")
    .AssertStatus(HttpStatusCode.OK)
    .AssertContainsHeader("Content-Type", "application/json")
    .AssertJson(new
        {
            type = Matchers.String,
            name = "claim",
            version = Matchers.Regex("^1")
        })
    .Execute();
```

### Request method support
Claim currently supports Get, Post, Put and Delete requests through the 
static Claim class e.g. Claim.Get(...).

### Asserts
##### AssertStatus
Asserts that the actual status code equals the expected one.

Example:
```
  Claims.Get("https://www.claim.com/api/v1/info")
    .AssertStatus(HttpStatusCode.OK)
    .Execute();
```
##### AssertContainsHeader
Asserts that http response contains the specified header and value.

Example:
```
  Claims.Get("https://www.claim.com/api/v1/info")
    .AssertContainsHeader("Content-Type", "application/json")
    .Execute();
```

##### AssertJson
Asserts that http response matches the expected json structure and/or values and types.

Example:
```
  Claims.Get("https://www.claim.com/api/v1/info")
    .AssertJson(new
      {
        // Macthes any string
        type = Matchers.String,
        // Matches the exact string "claim"
        name = "claim",
        // Matches anything that starts with "1"
        version = Matchers.Regex("^1")
      })
    .Execute();
```

