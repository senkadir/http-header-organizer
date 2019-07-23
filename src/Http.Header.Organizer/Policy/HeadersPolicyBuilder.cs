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
            bool exists = policy.HttpHeaders.Any(x => x.Key == key);

            if (exists)
            {
                return this;
            }

            HttpHeader header = new HttpHeader
            {
                Key = key,
                IsRequired = true,
                DefaultValue = defaultValue,
                IsRequestHeader = true
            };

            policy.HttpHeaders.Add(header);

            return this;
        }

        public HeadersPolicyBuilder AddCustomHeaderToResponses(string key, string value = "")
        {
            bool exists = policy.HttpHeaders.Any(x => x.Key == key);

            if (exists)
            {
                return this;
            }

            HttpHeader header = new HttpHeader
            {
                Key = $"{key}",
                DefaultValue = value,
                IsRequestHeader = false,
                IsRequired = true
            };

            policy.HttpHeaders.Add(header);

            return this;
        }

        public HeadersPolicy Build()
        {
            return policy;
        }
    }
}
