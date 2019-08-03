using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Http.Header.Organizer.Tests.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        public ValuesController()
        {
        }

        [HttpGet]
        public IActionResult Get()
        {
            var headers = HttpContext.Request.Headers;

            List<string> val = new List<string>();

            foreach (var keys in headers.Keys)
            {
                val.Add(keys + " : " + headers[keys].ToString());
            }

            var result = new
            {
                val
            };

            return Ok(result);
        }
    }
}
