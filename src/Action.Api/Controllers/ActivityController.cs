using System;
using System.Threading.Tasks;
using Action.Api.Services;
using Action.Common.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RawRabbit;

namespace Action.Api.Controllers {
    [Authorize]
    [ApiController]
    [Route ("[controller]")]
    public class ActivityController : ControllerBase {
        private readonly IBusClient _busClient;
        private readonly IActivityService _activityService;
        public ActivityController (IBusClient busClient, IActivityService activityService) 
        {
            _activityService = activityService;
            _busClient = busClient;
        }

        [HttpPost]
        public async Task<IActionResult> Activity (CreateActivity command) {
            command.Id = Guid.NewGuid ();
            command.CreatedAt = DateTime.Now;
            command.UserId = Guid.Parse(User.Identity.Name);
            await _busClient.PublishAsync (command);

            return Accepted ($"activities/{command.Id}");
        }

        [HttpGet]
        public async Task<IActionResult> GetAction() 
        {
            var activities = await _activityService.GetActivities(Guid.Parse(User.Identity.Name));
            return Ok(activities);
        }

        [HttpGet("{activityId}")]
        public async Task<IActionResult> GetAction(Guid activityId) 
        {
            var activity = await _activityService.GetActivityById(activityId);

            if (activity == null)
            {
                return NotFound();
            }

            if(activity.UserId != Guid.Parse(User.Identity.Name))
            {
                return Unauthorized();
            }

            return Ok(activity);
        }
    }
}