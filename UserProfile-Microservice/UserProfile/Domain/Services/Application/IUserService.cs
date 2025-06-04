using DittoBox.API.UserProfile.Domain.Models.Entities;

namespace DittoBox.API.UserProfile.Domain.Services.Application
{
    public interface IUserService
    {
        public Task<User> CreateUser(string username, string email, string password);
        public Task<User?> GetUser(int id);
        public Task DeleteUser(int id);
        public string EncryptPassword(string password);
        public Task UpdateUser(User user);
		public Task<IEnumerable<User>> GetUsers();
        public Task<string?> Login(string email, string password);
        public Task<User?> GetUserByEmail(string email);
		public Task<User?> GetUserByUsername(string username);

    }
}
