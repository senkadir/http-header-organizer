# http-header-organizer
HTTP Organizer is a middleware that checks all HTTP requests and responses in order to determine which policies to apply.

# Package

[Nuget package](https://www.nuget.org/packages/Http.Header.Organizer)

# Usage

Inject organizer to middleware with:

app.UseHeadersOrganizer();

Create your header policy with:

```csharp
app.UseHeadersOrganizer(x => x.AddRemoveHeaderToRequests("X-Test")
                              .AddRequiredHeaderToRequests("X-Test2")
                              .AddRequiredHeaderToRequests("X-Test-With-Default-Value", "With default value")
                              .AddCustomHeaderToResponses("X-Response-Test", "Response Test"));
```

That's it. Organizer will follow all requests and responses to apply your policies.

In this example:

1. Organizer will remove "X-Test" header which comes from in HttpRequest. 

2. Organizer add "X-Test2" required header policy. If header not found in http request returns 400 Bad Requests.

3. Organizer add "X-Test-With-Default-Value" required header policy with default value. If header not found in http request will be added with value: "With default value".

4. Organizer add "X-Response-Test" header to all responses with "Response Test" value.


###Developed by Abdulkadir Sen.
