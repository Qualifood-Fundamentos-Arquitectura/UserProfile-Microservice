using DittoBox.API.UserProfile.Domain.Models.Entities;
using DittoBox.API.UserProfile.Domain.Models.ValueObjects;
using DittoBox.API.UserProfile.Domain.Repositories;
using DittoBox.API.UserProfile.Domain.Services.Application;

namespace DittoBox.API.UserProfile.Application.Services
{
    public class ProfileService(
        IProfileRepository profileRepository,
        IProfilePrivilegeRepository profilePrivilegeRepository
        ) : IProfileService
    {
        public async Task<Profile> CreateProfile(int userId, string firstname, string lastname)
        {
            var profile = new Profile(userId, firstname, lastname);
            await profileRepository.Add(profile);
            return profile;
        }

        public async Task DeleteProfile(int profileId)
        {
            var profile = await profileRepository.GetById(profileId);
            if (profile != null)
            {
                await profileRepository.Delete(profile);
            }
        }

        public async Task<Profile?> GetProfile(int profileId)
        {
            return await profileRepository.GetById(profileId);
        }

        public Task<IEnumerable<Profile>> GetProfilesByAccountId(int accountId)
        {
            return profileRepository.GetByAccountId(accountId);
            
        }

        public async Task GrantPrivilege(ProfilePrivilege profilePrivilege)
        {
            if (await profilePrivilegeRepository.SamePrivilegeExists(profilePrivilege))
            {
                return;
            }

            await profilePrivilegeRepository.Add(profilePrivilege);
        }

        public async Task<ICollection<Privilege>> ListUserPrivileges(int profileId)
        {
            var profilePrivileges = await profilePrivilegeRepository.GetAllByProfileId(profileId);
            return profilePrivileges.Select(pp => pp.Privilege).ToList();
        }

        public async Task RevokePrivilege(ProfilePrivilege profilePrivilege)
        {
            if (await profilePrivilegeRepository.SamePrivilegeExists(profilePrivilege))
            {
                var existentPrivileges = await profilePrivilegeRepository.GetAllByProfileId(profilePrivilege.ProfileId);
                var toDelete = existentPrivileges.FirstOrDefault(pp => pp.Privilege == profilePrivilege.Privilege);

                if (toDelete != null)
                {
                    await profilePrivilegeRepository.Delete(toDelete);
                }
            }
        }

        public async Task UpdateProfile(Profile profile)
        {
            await profileRepository.Update(profile);
        }
    }
}
