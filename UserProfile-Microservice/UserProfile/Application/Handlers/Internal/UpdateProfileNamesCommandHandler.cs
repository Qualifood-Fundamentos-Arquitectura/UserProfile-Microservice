using DittoBox.API.Shared.Domain.Repositories;
using DittoBox.API.UserProfile.Application.Commands;
using DittoBox.API.UserProfile.Application.Handlers.Interfaces;
using DittoBox.API.UserProfile.Domain.Services.Application;

namespace DittoBox.API.UserProfile.Application.Handlers.Internal
{
    public class UpdateProfileNamesCommandHandler(
        IProfileService profileService,
        IUnitOfWork unitOfWork
        ) : IUpdateProfileNamesCommandHandler
    {
        public async Task Handle(UpdateProfileNamesCommand command)
        {
            var profile = await profileService.GetProfile(command.ProfileId);
            if (profile != null)
            {
                profile.FirstName = command.FirstName;
                profile.LastName = command.LastName;
                await profileService.UpdateProfile(profile);
                await unitOfWork.CompleteAsync();
            }
        }
    }
}
