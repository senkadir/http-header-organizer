using System;
using System.Collections.Generic;
using System.Text;

namespace Http.Header.Organizer
{
    public class HttpHeader
    {
        public string Key { get; set; }

        public bool IsRequired { get; set; }

        public string DefaultValue { get; set; }

        public bool IsRequestHeader { get; set; }
    }
}
