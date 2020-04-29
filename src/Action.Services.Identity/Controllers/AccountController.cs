using System.Threading.Tasks;
using Action.Common.Commands;
using Action.Services.Identity.Services;
using Microsoft.AspNetCore.Mvc;

namespace Action.Services.Identity.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<ActionResult> LoginTest(AuthenticateUser command)
            =>Ok( await _userService.LoginAsync(command.Email, command.Password));

        [HttpPost("login")]
        public async Task<IActionResult> Login(AuthenticateUser command)
            =>Ok( await _userService.LoginAsync(command.Email, command.Password));
    }
}