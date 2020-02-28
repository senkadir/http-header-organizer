using System.Linq;

namespace Http.Header.Organizer
{
    public class HeadersPolicyBuilder
    {
        private HeadersPolicy policy;

        public HeadersPolicyBuilder()
        {
            policy = new HeadersPolicy();
        }

        public HeadersPolicyBuilder AddRequiredHeaderToRequests(string key, string defaultValue = "")
        {
            if (policy.HttpRequestHeaders.Any(x => x.Key == key))
            {
                return this;
            }

            HttpRequestHeader header = new HttpRequestHeader
            {
                Key = key,
                IsRequired = true,
                DefaultValue = defaultValue
            };

            policy.HttpRequestHeaders.Add(header);

            return this;
        }

        public HeadersPolicyBuilder AddCustomHeaderToResponses(string key, string value = "")
        {
            if (policy.HttpResponseHeaders.Any(x => x.Key == key))
            {
                return this;
            }

            HttpResponseHeader header = new HttpResponseHeader
            {
                Key = $"{key}",
                DefaultValue = value,
                IsRequired = true
            };

            policy.HttpResponseHeaders.Add(header);

            return this;
        }

        public HeadersPolicyBuilder AddRemoveHeaderToRequests(string key)
        {
            if (policy.RemoveHeaders.Any(x => x.Key == key))
            {
                return this;
            }

            HttpHeader header = new HttpHeader
            {
                Key = key,
            };

            policy.RemoveHeaders.Add(header);

            return this;
        }

        public HeadersPolicy Build()
        {
            return policy;
        }
    }
}
