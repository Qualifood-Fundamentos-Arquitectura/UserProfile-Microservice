using DittoBox.API.Shared.Infrastructure;
using DittoBox.API.Shared.Infrastructure.Repositories;
using DittoBox.API.UserProfile.Domain.Models.ValueObjects;
using DittoBox.API.UserProfile.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DittoBox.API.UserProfile.Infrastructure.Repositories
{
    public class ProfilePrivilegeRepository : BaseRepository<ProfilePrivilege>, IProfilePrivilegeRepository
    {
        private readonly ApplicationDbContext _context;

        public ProfilePrivilegeRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<ICollection<ProfilePrivilege>> GetAllByProfileId(int profileId)
        {
            return await _context.Set<ProfilePrivilege>().Where(pp => pp.ProfileId == profileId).ToListAsync();
        }

        public async Task<bool> SamePrivilegeExists(ProfilePrivilege profilePrivilege)
        {
            var exists = await _context.Set<ProfilePrivilege>().AnyAsync(pp => pp.ProfileId == profilePrivilege.ProfileId && pp.Privilege == profilePrivilege.Privilege);
            return exists;
        }
    }
}