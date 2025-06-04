using DittoBox.API.Shared.Domain.Repositories;
using DittoBox.API.UserProfile.Domain.Models.ValueObjects;

namespace DittoBox.API.UserProfile.Domain.Repositories
{
    public interface IProfilePrivilegeRepository : IBaseRepository<ProfilePrivilege>
    {
        public Task<bool> SamePrivilegeExists(ProfilePrivilege profilePrivilege);
        public Task<ICollection<ProfilePrivilege>> GetAllByProfileId(int profileId);
    }
}
