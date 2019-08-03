using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections.Generic;
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
            try
            {
                ProcessRemoveHeaders(context.Request.Headers, policy.RemoveHeaders);

                ProcessRequestHeaders(context, policy.HttpRequestHeaders);

                ProcessResponseHeaders(context, policy.HttpResponseHeaders);
            }
            catch (Exception ex)
            {

                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

                await context.Response.WriteAsync(ex.Message);
                context.Response.ContentType = @"application/json";

                return;
            }

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

        private void ProcessRequestHeaders(HttpContext context, List<HttpRequestHeader> httpHeaders)
        {
            IHeaderDictionary requestHeaders = context.Request.Headers;

            foreach (var header in httpHeaders)
            {
                if (requestHeaders.ContainsKey(header.Key))
                {
                    continue;
                }

                if (header.IsRequired && string.IsNullOrEmpty(header.DefaultValue))
                {
                    logger.LogError($"Required header: {header.Key} not found.");

                    throw new Exception($"The request header key not found in headers.");
                }

                if (header.IsRequired && string.IsNullOrEmpty(header.DefaultValue) == false)
                {
                    requestHeaders.Add(header.Key, header.DefaultValue);

                    logger.LogInformation($"Header not found. Added with default value. {header.Key}:{header.DefaultValue}");
                }
            }
        }

        private void ProcessResponseHeaders(HttpContext context, List<HttpResponseHeader> httpHeaders)
        {
            IHeaderDictionary responseHeaders = context.Response.Headers;

            foreach (var header in httpHeaders)
            {
                if (responseHeaders.ContainsKey(header.Key))
                {
                    continue;
                }

                if (header.IsRequired && string.IsNullOrEmpty(header.DefaultValue))
                {
                    logger.LogError($"Required header: {header.Key} not found.");

                    throw new Exception($"The response header key not found in headers.");
                }

                if (header.IsRequired && string.IsNullOrEmpty(header.DefaultValue) == false)
                {
                    context.Response.OnStarting(() =>
                    {
                        context.Response.Headers.Add(header.Key, header.DefaultValue);

                        return Task.CompletedTask;

                    });

                    logger.LogInformation($"Header not found. Added with default value. {header.Key}:{header.DefaultValue}");
                }
            }
        }
    }
}
