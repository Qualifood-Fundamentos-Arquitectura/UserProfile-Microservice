using DittoBox.API.UserProfile.Application.Commands;

namespace DittoBox.API.UserProfile.Application.Handlers.Interfaces
{
    public interface IChangePasswordCommandHandler
    {
        public Task Handle(ChangePasswordCommand command);
    }
}
