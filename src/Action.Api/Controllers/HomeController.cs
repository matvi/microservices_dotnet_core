using Microsoft.AspNetCore.Mvc;

namespace Action.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAction() => Content("Hello from Actino API");
    }
}