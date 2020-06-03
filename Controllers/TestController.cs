using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreJsonTests.Controllers
{
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpGet]
        [Route("/")]
        public IActionResult Get([FromQuery] TestObject request)
        {
            return Ok(request);
        }

        [HttpPost]
        [Route("/")]
        public IActionResult Post([FromBody] TestObject request)
        {
            return Ok(request);
        }
    }
}
