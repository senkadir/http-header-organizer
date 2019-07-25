using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Http.Header.Organizer
{
    public class OrganizerMiddleware
    {
        private readonly ILogger<OrganizerMiddleware> logger;

        private readonly RequestDelegate next;

        private readonly HeadersPolicy policy;

        public OrganizerMiddleware(RequestDelegate nextDelegate,
                                   HeadersPolicy headersPolicy,
                                   ILogger<OrganizerMiddleware> organizerLogger = null)
        {
            logger = organizerLogger ?? NullLogger<OrganizerMiddleware>.Instance;
            next = nextDelegate;
            policy = headersPolicy;
        }

        public async Task Invoke(HttpContext context)
        {
            IHeaderDictionary responseHeaders = context.Response.Headers;

            ProcessRemoveHeaders(responseHeaders, policy.RemoveHeaders);

            await ProcessRequestHeadersAsync(context, policy.HttpRequestHeaders);

            await next(context);
        }

        private void ProcessRemoveHeaders(IHeaderDictionary headers, List<HttpHeader> headerstoRemove)
        {
            foreach (var header in headerstoRemove)
            {
                if (headers.ContainsKey(header.Key))
                {
                    headers.Remove(header.Key);

                    logger.LogInformation($"Header removed. Header key: {header.Key}");
                }
            }
        }

        private async Task ProcessRequestHeadersAsync(HttpContext context, List<HttpRequestHeader> httpHeaders)
        {
            IHeaderDictionary requestHeaders = context.Request.Headers;

            foreach (var header in httpHeaders)
            {
                if (requestHeaders.ContainsKey(header.Key))
                {
                    return;
                }

                if (header.IsRequired && string.IsNullOrEmpty(header.DefaultValue))
                {
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    context.Response.ContentType = @"application/json";

                    await context.Response.WriteAsync($"The request header key not found in headers.");
                }

                if (header.IsRequired && string.IsNullOrEmpty(header.DefaultValue) == false)
                {
                    requestHeaders.Add(header.Key, header.DefaultValue);

                    logger.LogInformation($"Header not found. Added with default value. {header.Key}:{header.DefaultValue}");
                }
            }
        }
    }
}
