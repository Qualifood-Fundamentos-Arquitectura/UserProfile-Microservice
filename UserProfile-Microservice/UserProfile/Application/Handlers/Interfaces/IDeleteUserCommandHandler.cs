using DittoBox.API.UserProfile.Application.Commands;

namespace DittoBox.API.UserProfile.Application.Handlers.Interfaces
{
    public interface IDeleteUserCommandHandler
    {
        public Task Handle(DeleteUserCommand command);
    }
}
