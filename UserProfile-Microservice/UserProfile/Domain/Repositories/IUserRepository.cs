using DittoBox.API.Shared.Domain.Repositories;
using DittoBox.API.UserProfile.Domain.Models.Entities;

namespace DittoBox.API.UserProfile.Domain.Repositories {
	public interface IUserRepository : IBaseRepository<User> {
		public Task<User?> GetByEmail(string email);
		public Task<User?> GetByUsername(string username);
    }
}