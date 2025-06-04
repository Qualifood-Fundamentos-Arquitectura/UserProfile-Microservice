using DittoBox.API.UserProfile.Application.Commands;

namespace DittoBox.API.UserProfile.Application.Handlers.Interfaces
{
    public interface IGrantPrivilegeCommandHandler
    {
        public Task Handle(GrantPrivilegeCommand command);
    }
}
