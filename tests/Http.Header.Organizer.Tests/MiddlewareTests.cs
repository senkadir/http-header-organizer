using Microsoft.AspNetCore.Http;
using Xunit;

namespace Http.Header.Organizer.Tests
{
    public class MiddlewareTests
    {
        [Fact]
        public async void Test_Remove_Request_Header_or_Headers_In_HttpRequest_Async()
        {
            //Arrange
            //HttpContext testContext;

            //testContext.Request.Headers.Clear();

            //testContext.Request.Headers.Add("X-Test-Header", "Test value");

            //RequestDelegate @delegate = new RequestDelegate(testContext);

            //HeadersPolicy testPolicy = new HeadersPolicy();

            //testPolicy.RemoveHeaders.Add(new HttpHeader
            //{
            //    Key = "X-Test-Header"
            //});

            //OrganizerMiddleware organizerMiddleware = new OrganizerMiddleware(@delegate, testPolicy);

            //Act
            //await organizerMiddleware.Invoke(testContext);

            //Assert
            //Assert.Equal(0, testContext.Request.Headers.Count);
        }
    }
}
