using Microsoft.AspNetCore.Builder;

namespace Http.Header.Organizer
{
    public static class Extensions
    {
        public static IApplicationBuilder UseHeadersOrganizer(this IApplicationBuilder app, HeadersPolicyBuilder configure)
        {
            HeadersPolicy policy = configure.Build();

            return app.UseMiddleware<OrganizerMiddleware>(policy);
        }
    }
}
