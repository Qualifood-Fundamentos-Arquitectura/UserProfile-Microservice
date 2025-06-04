using DittoBox.API.UserProfile.Domain.Models.Entities;
using DittoBox.API.UserProfile.Domain.Models.ValueObjects;

namespace DittoBox.API.UserProfile.Domain.Services.Application
{
    public interface IProfileService
    {
        public Task<Profile?> GetProfile(int profileId);
        public Task<Profile> CreateProfile(int userId, string firstname, string lastname);
        public Task DeleteProfile(int profileId);
        public Task UpdateProfile(Profile profile);
        public Task GrantPrivilege(ProfilePrivilege profilePrivilege);
        public Task RevokePrivilege(ProfilePrivilege profilePrivilege);
        public Task<ICollection<Privilege>> ListUserPrivileges(int userId);

        public Task<IEnumerable<Profile>> GetProfilesByAccountId(int accountId);
    }
}
