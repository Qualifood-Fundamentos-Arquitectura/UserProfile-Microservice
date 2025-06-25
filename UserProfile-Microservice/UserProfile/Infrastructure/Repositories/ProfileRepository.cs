using DittoBox.API.Shared.Infrastructure;
using DittoBox.API.Shared.Infrastructure.Repositories;
using DittoBox.API.UserProfile.Domain.Models.Entities;
using DittoBox.API.UserProfile.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DittoBox.API.UserProfile.Infrastructure.Repositories
{
    public class ProfileRepository : BaseRepository<Profile>, IProfileRepository
    {
        private readonly ApplicationDbContext _context;

        public ProfileRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Profile>> GetByAccountId(int accountId)
        {
            return await _context.Profiles.Where(p => p.AccountId == accountId).Include(p => p.ProfilePrivileges).ToListAsync();
        }
        
        public new async Task<Profile?> GetById(int id)
        {
            return await _context.Profiles
                .Include(p => p.ProfilePrivileges)
                .FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}
