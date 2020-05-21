using System;
using System.Threading.Tasks;
using Action.Common.Commands;
using Action.Common.Events;
using Action.Common.Exceptions;
using Action.Services.Activities.Services;
using Microsoft.Extensions.Logging;
using RawRabbit;

namespace Action.Services.Activities.Handlers {
    public class CreateActivityHandler : ICommandHandler<CreateActivityCommand> {
        private readonly IBusClient _busClient;
        private readonly IActivityService _activityService;
        private ILogger _logger;

        public CreateActivityHandler (IBusClient busClient, 
            IActivityService activityService,
            ILogger<CreateActivityHandler> logger) {
            _activityService = activityService;
            _busClient = busClient;
            _logger = logger;
        }
        public async Task HandlerAsync (CreateActivityCommand command) 
        {
            _logger.LogInformation($"creating activity: {command.Name}, {DateTime.Now.ToString()} ");
            
            try
            {
                await _activityService.AddAsync(command.Id, command.UserId, command.Category, command.Name, command.Description, command.CreatedAt);
                //execute the handler
                await _busClient.PublishAsync (new ActivityCreated (
                    command.Id, command.UserId, command.Category, command.Name, command.Description
                 ));
                 return;
            }
            catch (ActioException ex)
            {
                await _busClient.PublishAsync(new CreateActivityRejected(command.Id, ex.Code, ex.Message));
                _logger.LogError(ex.Message);
            }
            catch (Exception ex)
            {
                await _busClient.PublishAsync(new CreateActivityRejected(command.Id, "Error", ex.Message));
                _logger.LogError(ex.Message);
            }
        }
    }
}