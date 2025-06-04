using DittoBox.API.UserProfile.Application.Commands;

namespace DittoBox.API.UserProfile.Application.Handlers.Interfaces
{
    public interface IRevokePrivilegeCommandHandler
    {
        public Task Handle(RevokePrivilegeCommand command);
    }
}
