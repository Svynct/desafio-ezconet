using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [HttpGet, Route("")]
        public ActionResult Get()
        {
            return Ok("WebAPI");
        }
    }
}
