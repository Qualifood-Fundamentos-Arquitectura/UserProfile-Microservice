using DittoBox.API.Shared.Infrastructure;
using DittoBox.API.Shared.Infrastructure.Repositories;
using DittoBox.API.UserProfile.Domain.Models.Entities;
using DittoBox.API.UserProfile.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DittoBox.API.UserProfile.Infrastructure.Repositories
{
    public class UserRepository(ApplicationDbContext context) : BaseRepository<User>(context), IUserRepository
    {

        public async Task<User?> GetByEmail(string email)
        {
            return await context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

		public Task<User?> GetByUsername(string username)
		{
			return context.Users.FirstOrDefaultAsync(u => u.Username == username);
		}
	}
}
