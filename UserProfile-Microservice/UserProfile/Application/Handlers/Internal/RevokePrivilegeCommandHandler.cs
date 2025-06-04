using DittoBox.API.Shared.Domain.Repositories;
using DittoBox.API.UserProfile.Application.Commands;
using DittoBox.API.UserProfile.Application.Handlers.Interfaces;
using DittoBox.API.UserProfile.Domain.Models.ValueObjects;
using DittoBox.API.UserProfile.Domain.Services.Application;

namespace DittoBox.API.UserProfile.Application.Handlers.Internal
{
    public class RevokePrivilegeCommandHandler(
        IProfileService profileService,
        IUnitOfWork unitOfWork
        ) : IRevokePrivilegeCommandHandler
    {
        public async Task Handle(RevokePrivilegeCommand command)
        {
            var profilePrivilege = new ProfilePrivilege(command.ProfileId, (Privilege)command.PrivilegeId);
            await profileService.RevokePrivilege(profilePrivilege);
            await unitOfWork.CompleteAsync();
        }
    }
}
