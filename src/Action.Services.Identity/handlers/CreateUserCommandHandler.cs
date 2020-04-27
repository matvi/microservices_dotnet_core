using System;
using System.Threading.Tasks;
using Action.Common.Commands;
using Action.Common.Events;
using Action.Common.Exceptions;
using Action.Services.Identity.Services;
using Microsoft.Extensions.Logging;
using RawRabbit;

namespace Action.Services.Identity.handlers {
    public class CreateUserCommandHandler : ICommandHandler<CreateUserCommand> {
        private readonly IUserService _userService;
        private readonly IBusClient _bus;
        private readonly ILogger<CreateUserCommandHandler> _logger;
        public CreateUserCommandHandler (IBusClient bus, IUserService userService, ILogger<CreateUserCommandHandler> logger) {
            _logger = logger;
            _bus = bus;
            _userService = userService;

        }
        public async Task HandlerAsync (CreateUserCommand command) {
            _logger.LogInformation($"Creating user: {command.Email}");

            try
            {
                await _userService.RegisterAsync (command.Email, command.Password, command.Username);
                await _bus.PublishAsync (new UserCreated (command.Email, command.Username));
                return;
            }
            catch (ActioException err)
            {
                _logger.LogError("Couldn´t register user " + err.Message);
                await _bus.PublishAsync( new CreateUserRejected(err.Message, err.Code, command.Email));
            }
            catch (Exception err)
            {
                _logger.LogError("Couldn´t register user " + err.Message);
                await _bus.PublishAsync( new CreateUserRejected(err.Message, "1001", command.Email));
            }

        }
    }
}