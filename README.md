<p align="center">
  <strong>HTTP Header Organizer</strong>
</p>

HTTP Header Organizer is a C# middleware library that intercepts and analyzes all incoming and outgoing HTTP traffic in order to determine which policies to apply.

## How it works
The middleware is integrated into the application stack and checks all HTTP requests and responses. It analyzes the traffic and applies policies, defined in the UseHeadersOrganizer method in the Startup.

## Usage

Install package from Nuget:

```csharp
Install-Package Http.Header.Organizer
```

or from dotnet cli:

```csharp
dotnet add package Http.Header.Organizer
```

Inject organizer to middleware with:

```csharp
  app.UseHeadersOrganizer();
```

Create your header policy with:

```csharp
app.UseHeadersOrganizer(x => x.AddRemoveHeaderToRequests("X-Test")
                              .AddRequiredHeaderToRequests("X-Test2")
                              .AddRequiredHeaderToRequests("X-Test-With-Default-Value", "With default value")
                              .AddCustomHeaderToResponses("X-Response-Test", "Response Test"));
```

That's it. Organizer will follow all requests and responses to apply your policies.

In this example:

1. The Organizer will remove the "X-Test" header from the incoming HTTP request.

2. The Organizer will enforce a required header policy for "X-Test2". If this header is not found in the incoming HTTP request, the Organizer will return a 400 Bad Request response.

3. The Organizer will enforce a required header policy with a default value for "X-Test-With-Default-Value". If this header is not found in the incoming HTTP request, the Organizer will add it to the request with a default value of "With default value".

4. The Organizer will add the "X-Response-Test" header with a value of "Response Test" to all outgoing HTTP responses.

## Nuget Package

[Nuget package](https://www.nuget.org/packages/Http.Header.Organizer)


Developed by Abdulkadir Sen.
