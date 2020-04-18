using System;
using System.Threading.Tasks;
using Action.Common.Commands;
using Microsoft.AspNetCore.Mvc;
using RawRabbit;

namespace Action.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IBusClient _busClient;
        public UserController(IBusClient busClient)
        {
            _busClient = busClient;
        }

        [HttpPost]
        public async Task<IActionResult> Activity(CreateUserCommand command)
        {
            await _busClient.PublishAsync(command);

            return Accepted();
        }
    }
}