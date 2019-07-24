using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
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
            IHeaderDictionary requestHeaders = context.Request.Headers;

            var requestHeadersPolicies = policy.HttpHeaders.Where(x => x.IsRequestHeader == true);

            foreach (var header in requestHeadersPolicies)
            {
                bool keyExists = requestHeaders.ContainsKey(header.Key);

                if (keyExists == false && header.IsRequired == true)
                {
                    if (string.IsNullOrEmpty(header.DefaultValue))
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        context.Response.ContentType = @"application/json";
                        await context.Response.WriteAsync($"The request header key not found in headers.");
                    }
                    else
                    {
                        requestHeaders.Add(header.Key, header.DefaultValue);
                    }
                }
            }

            IHeaderDictionary responseHeaders = context.Response.Headers;

            var responseHeadersPolicies = policy.HttpHeaders.Where(x => x.IsRequestHeader == false);

            foreach (var header in responseHeadersPolicies)
            {
                bool keyExists = requestHeaders.ContainsKey(header.Key);

                if (keyExists == false && header.IsRequired == true)
                {
                    responseHeaders.Add(header.Key, header.DefaultValue);
                }
            }

            await next(context);
        }
    }
}
