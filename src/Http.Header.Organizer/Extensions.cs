using Microsoft.AspNetCore.Builder;
using System;

namespace Http.Header.Organizer
{
    public static class Extensions
    {
        public static IApplicationBuilder UseHeadersOrganizer(this IApplicationBuilder app, Action<HeadersPolicyBuilder> configure = null)
        {
            if (configure == null)
            {
                throw new ArgumentNullException(nameof(configure));
            }

            HeadersPolicyBuilder policyBuilder = new HeadersPolicyBuilder();

            configure?.Invoke(policyBuilder);

            var policy = policyBuilder.Build();

            return app.UseMiddleware<OrganizerMiddleware>(policy);
        }

        public static IApplicationBuilder UseHeadersOrganizer(this IApplicationBuilder app, HeadersPolicy policy)
        {
            return app.UseMiddleware<OrganizerMiddleware>(policy);
        }
    }
}
