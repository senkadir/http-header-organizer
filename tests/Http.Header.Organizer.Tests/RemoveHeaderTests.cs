using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace Http.Header.Organizer.Tests
{
    public class RemoveHeaderTests
    {
        [Fact]
        public async void Test_Remove_Request_Header_or_Headers_In_HttpRequest_Async()
        {
            //Arrange
            string testHeaderKey = "X-Test-Header-To-Remove";

            HeadersPolicyBuilder builder = new HeadersPolicyBuilder();

            builder.AddRemoveHeaderToRequests(testHeaderKey);

            OrganizerMiddleware middleware = new OrganizerMiddleware(nextDelegate: (innerHttpContext) =>
            {
                innerHttpContext.Request.Headers.Add(testHeaderKey, "Test Value");

                return Task.CompletedTask;
            }, builder.Build());

            DefaultHttpContext context = new DefaultHttpContext();

            context.Response.Body = new MemoryStream();

            //Act
            await middleware.InvokeAsync(context);

            //Assert
            Assert.False(context.Response.Headers.ContainsKey(testHeaderKey));
        }
    }
}
