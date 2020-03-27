using Microsoft.AspNetCore.Mvc;

namespace Action.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAction() => Content("Hellow from Actino API");
    }
}