using System.Threading.Tasks;
using Action.Common.Events;

namespace Action.Api.Handlers
{
    public class ActivityCreatedHandler : IEventHandler<ActivityCreated>
    {
        public async Task HandlerAsync(ActivityCreated events)
        {
            await Task.CompletedTask;
            System.Console.WriteLine($"Activity created: {events.Name}");
        }
    }
}