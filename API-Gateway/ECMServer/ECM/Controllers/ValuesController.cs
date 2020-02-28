using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECM.Controllers
{
    [Route("api/[controller]")]
    [Authorize, ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [AllowAnonymous, HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "ECM1", "ECM2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "ECM";
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody] string value)
        {
            return Ok(new { status = 200, message = "success", data = value });
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
