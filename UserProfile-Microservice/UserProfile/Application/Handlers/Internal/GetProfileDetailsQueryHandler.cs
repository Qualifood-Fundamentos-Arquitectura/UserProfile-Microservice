using DittoBox.API.UserProfile.Application.Handlers.Interfaces;
using DittoBox.API.UserProfile.Application.Queries;
using DittoBox.API.UserProfile.Application.Resources;
using DittoBox.API.UserProfile.Domain.Repositories;
using DittoBox.API.UserProfile.Domain.Services.Application;

namespace DittoBox.API.UserProfile.Application.Handlers.Internal
{
    public class GetProfileDetailsQueryHandler(
        IProfileService profileService
        ) : IGetProfileDetailsQueryHandler
    {
        public async Task<ProfileResource?> Handle(GetProfileQuery query)
        {
            var result = await profileService.GetProfile(query.ProfileId);
            if (result == null)
            {
                return null;
            }
            var privileges = await profileService.ListUserPrivileges(query.ProfileId);
            return new ProfileResource()
            {
                AccountId = result.AccountId,
                GroupId = result.GroupId,
                FirstName = result.FirstName,
                LastName = result.LastName,
                Privileges = privileges.Select(p => p.ToString()).ToArray()
            };
        }
    }
}
