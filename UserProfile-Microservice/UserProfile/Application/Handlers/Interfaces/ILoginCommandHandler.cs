using DittoBox.API.UserProfile.Application.Commands;
using DittoBox.API.UserProfile.Application.Resources;

namespace DittoBox.API.UserProfile.Application.Handlers.Interfaces
{
    public interface ILoginCommandHandler
    {
        public Task<LoginResource?> Handle(LoginCommand command);
    }
}
