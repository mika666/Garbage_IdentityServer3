using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Api.Controllers
{
    [Route("values")]
    public class ValuesController : ApiController
    {
        public IEnumerable<string> Get()
        {
            var random = new Random();

            return new[]
            {
            random.Next(0, 10).ToString(),
            random.Next(0, 10).ToString()
        };
        }
    }
}