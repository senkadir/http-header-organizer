using System.Collections.Generic;

namespace Http.Header.Organizer
{
    public class HeadersPolicy
    {
        public List<HttpRequestHeader> HttpRequestHeaders { get; } = new List<HttpRequestHeader>();

        public List<HttpResponseHeader> HttpResponseHeaders { get; } = new List<HttpResponseHeader>();

        public List<HttpHeader> RemoveHeaders { get; set; } = new List<HttpHeader>();
    }
}
