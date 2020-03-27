using System.Threading.Tasks;
using Action.Common.Commands;
using Action.Common.Events;
using RawRabbit;

namespace Action.Services.Activities.Handlers
{
    public class CreateActivityHandler : ICommandHandler<CreateActivity>
    {
        private readonly IBusClient _busClient;

        public CreateActivityHandler(IBusClient busClient)
        {
            _busClient = busClient;
        }
        public async Task HandlerAsync(CreateActivity command)
        {
            System.Console.WriteLine($"creating activity: {command.Name}");

            //execute the handler
            await _busClient.PublishAsync(new ActivityCreated(
                command.Id, command.UserId, command.Category, command.Name, command.Description
            ));
        }
    }
}