using System.Collections.Generic;

namespace Http.Header.Organizer
{
    public class HeadersPolicy
    {
        public List<HttpRequestHeader> HttpRequestHeaders { get; } = new List<HttpRequestHeader>();

        public List<HttpRequestHeader> HttpResponseHeaders { get; } = new List<HttpRequestHeader>();

        public List<HttpHeader> RemoveHeaders { get; set; } = new List<HttpHeader>();
    }
}
