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
            bool exists = policy.HttpRequestHeaders.Any(x => x.Key == key);

            if (exists)
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
            bool exists = policy.HttpResponseHeaders.Any(x => x.Key == key);

            if (exists)
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
            bool exists = policy.RemoveHeaders.Any(x => x.Key == key);

            if (exists == true)
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
