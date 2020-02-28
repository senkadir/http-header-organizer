using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace Http.Header.Organizer.Tests
{
    public class RequiredHeaderTests
    {
        [Fact]
        public async void Test_Returns_400_If_Required_Header_Not_Found_In_Request()
        {
            //Arrange
            string testHeaderKey = "X-Test-Required-Header";

            HeadersPolicyBuilder builder = new HeadersPolicyBuilder();

            builder.AddRequiredHeaderToRequests(testHeaderKey);

            OrganizerMiddleware middleware = new OrganizerMiddleware(nextDelegate: (innerHttpContext) =>
            {
                return Task.CompletedTask;
            }, builder.Build());

            DefaultHttpContext context = new DefaultHttpContext();

            context.Response.Body = new MemoryStream();

            //Act
            await middleware.InvokeAsync(context);

            //Assert
            Assert.Equal(400, context.Response.StatusCode);
        }

        [Fact]
        public async void Test_Adds_Required_Header_And_Default_Value_If_Not_Found_In_Request()
        {
            //Arrange
            string testHeaderKey = "X-Test-Required-Header";

            HeadersPolicyBuilder builder = new HeadersPolicyBuilder();

            builder.AddRequiredHeaderToRequests(testHeaderKey, "test");

            OrganizerMiddleware middleware = new OrganizerMiddleware(nextDelegate: (innerHttpContext) =>
            {
                return Task.CompletedTask;
            }, builder.Build());

            DefaultHttpContext context = new DefaultHttpContext();

            context.Response.Body = new MemoryStream();

            //Act
            await middleware.InvokeAsync(context);

            //Assert
            Assert.Equal(200, context.Response.StatusCode);
            Assert.Equal("test", context.Request.Headers[testHeaderKey]);
            Assert.True(context.Request.Headers.ContainsKey(testHeaderKey));
        }
    }
}
