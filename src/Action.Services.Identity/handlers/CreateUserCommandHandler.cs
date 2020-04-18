using System.Threading.Tasks;
using Action.Common.Commands;

namespace Action.Services.Identity.handlers
{
    public class CreateUserCommandHandler : ICommandHandler<CreateUserCommand>
    {
        public Task HandlerAsync(CreateUserCommand command)
        {
            throw new System.NotImplementedException();
        }
    }
}