using DittoBox.API.Shared.Domain.Repositories;
using DittoBox.API.UserProfile.Application.Commands;
using DittoBox.API.UserProfile.Application.Handlers.Interfaces;
using DittoBox.API.UserProfile.Domain.Services.Application;

namespace DittoBox.API.UserProfile.Application.Handlers.Internal
{
    public class DeleteUserCommandHandler(
        IUserService userService,
        IProfileService profileService,
        IUnitOfWork unitOfWork
        ) : IDeleteUserCommandHandler
    {
        public async Task Handle(DeleteUserCommand command)
        {
            await userService.DeleteUser(command.UserId);
            var profile = await profileService.GetProfile(command.UserId);
            await profileService.DeleteProfile(profile!.Id);
            await unitOfWork.CompleteAsync();
        }
    }
}
