# http-header-organizer
This library inspired by:

Http Organizer is a middleware checking all Http Requests and Http Responses to decide to apply your policies.

# Usage

Inject organizer to middleware with:

app.UseHeadersOrganizer();

Create your header policy with:

app.UseHeadersOrganizer(x => x.AddRemoveHeaderToRequests("X-Test")
                                          .AddRequiredHeaderToRequests("X-Test2")
                                          .AddRequiredHeaderToRequests("X-Test-With-Default-Value", "With default value")
                                          .AddCustomHeaderToResponses("X-Response-Test", "Response Test"));

That's it. Organizer will follow all requests and responses to apply your policy.

In this example:

1. Organizer will remove "X-Test" header which comes from in HttpRequest. 

2. Organizer add "X-Test2" required header policy. If header not found in http request returns 400 Bad Requests.

3. Organizer add "X-Test-With-Default-Value" required header policy with default value. If header not found in http request will be added with value: "With default value".

4. Organizer add "X-Response-Test" header to all responses with "Response Test" value.
