using System;
using System.Threading.Tasks;
using Action.Common.Commands;
using Action.Common.Events;
using Action.Services.Activities.Services;
using RawRabbit;

namespace Action.Services.Activities.Handlers {
    public class CreateActivityHandler : ICommandHandler<CreateActivity> {
        private readonly IBusClient _busClient;
        private readonly IActivityService _activityService;

        public CreateActivityHandler (IBusClient busClient, IActivityService activityService) {
            _activityService = activityService;
            _busClient = busClient;
        }
        public async Task HandlerAsync (CreateActivity command) {
            System.Console.WriteLine ($"creating activity: {command.Name}, {DateTime.Now.ToString()} ");
            
            await _activityService.AddAsync(command.Id, command.UserId, command.Category, command.Name, command.Description, command.CreatedAt);

            //execute the handler
            await _busClient.PublishAsync (new ActivityCreated (
                command.Id, command.UserId, command.Category, command.Name, command.Description
            ));
        }
    }
}