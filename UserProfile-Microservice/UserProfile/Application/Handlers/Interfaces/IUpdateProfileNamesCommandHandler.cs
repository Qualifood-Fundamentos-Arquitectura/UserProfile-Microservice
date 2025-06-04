using DittoBox.API.UserProfile.Application.Commands;

namespace DittoBox.API.UserProfile.Application.Handlers.Interfaces
{
    public interface IUpdateProfileNamesCommandHandler
    {
        public Task Handle(UpdateProfileNamesCommand command);
    }
}
