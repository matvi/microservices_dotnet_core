using System;
using System.Threading.Tasks;
using Action.Common.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RawRabbit;

namespace Action.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ActivityController : ControllerBase
    {
        private readonly IBusClient _busClient;
        public ActivityController(IBusClient busClient)
        {
            _busClient = busClient;
        }

        [HttpPost]
        public async Task<IActionResult> Activity(CreateActivity command)
        {
            command.Id = Guid.NewGuid();
            command.CreatedAt = DateTime.Now;
            await _busClient.PublishAsync(command);

            return Accepted($"activities/{command.Id}");
        }
        
        [HttpGet]
        [Authorize]
        public IActionResult GetAction() => Content("secured");
    }
}