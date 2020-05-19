using System.Threading.Tasks;
using Action.Api.Domain.Model;
using Action.Api.Repositories;
using Action.Common.Events;

namespace Action.Api.Handlers
{
    public class ActivityCreatedHandler : IEventHandler<ActivityCreated>
    {
        private readonly IActivityRepository _activityRepository;
        public ActivityCreatedHandler(IActivityRepository activityRepository)
        {
            _activityRepository = activityRepository;

        }
        public async Task HandlerAsync(ActivityCreated activityCreated)
        {
            var activity = new Activity()
            {
                Id = activityCreated.Id,
                Category = activityCreated.Category,
                CreatedAt = activityCreated.CreatedAt,
                Description = activityCreated.Description,
                Name = activityCreated.Name,
                UserId = activityCreated.UserId
            };
            await _activityRepository.AddAsync(activity);
            System.Console.WriteLine($"Activity created: {activityCreated.Name}");
        }
    }
}