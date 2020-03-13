using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECM.Controllers
{
    [Route("api/[controller]")]
    [Authorize, ApiController]
    public class TestController : ControllerBase
    {
        // GET: api/Test
        [HttpGet]
        public IActionResult Get()
        {
            var datas = new[] {
                new { Group = 1, Id = 1, Name = "ECM1" },
                new { Group = 1, Id = 2, Name = "ECM2" },
                new { Group = 2, Id = 3, Name = "ECM3" },
                new { Group = 2, Id = 4, Name = "ECM4" },
            }.ToList();
            return Ok(datas);
        }

        // POST: api/Test
        [HttpPost]
        public IActionResult Post([FromBody] object data)
        {
            var result = $"Run success mothod post to ECMServer!";
            return Ok(new { status = 200, message = "success", data = result });
        }

        // POST: api/Test
        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody]object data)
        {
            var result = $"Run success mothod put/{id} to ECMServer!";
            return Ok(new { status = 200, message = "success", data = result });
        }

        // POST: api/Test
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var result = $"Run success mothod delete/{id} to ECMServer!";
            return Ok(new { status = 200, message = "success", data = result });
        }
    }
}
