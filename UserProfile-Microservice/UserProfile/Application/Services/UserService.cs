using DittoBox.API.Shared.Domain.Repositories;
using DittoBox.API.UserProfile.Domain.Models.Entities;
using DittoBox.API.UserProfile.Domain.Repositories;
using DittoBox.API.UserProfile.Domain.Services.Application;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace DittoBox.API.UserProfile.Application.Services
{
    public class UserService(
        IUserRepository userRepository,
        IConfiguration configuration
        ) : IUserService
    {
        public async Task<User> CreateUser(string username, string email, string password)
        {
            var user = new User(username, EncryptPassword(password), email);
            await userRepository.Add(user);
            return user;
        }

        public string EncryptPassword(string password)
        {
            var hashedBytes = SHA256.HashData(Encoding.UTF8.GetBytes(password));
            var hash = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            return hash;
        }

        public async Task<User?> GetUser(int id)
        {
            return await userRepository.GetById(id);
        }

        public async Task DeleteUser(int id)
        {
            var user = await userRepository.GetById(id);
            if (user != null)
            {
                await userRepository.Delete(user);
            }
        }

        public async Task UpdateUser(User user)
        {
            await userRepository.Update(user);
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            return await userRepository.GetAll();
        }

        public async Task<User?> GetUserByEmail(string email)
        {
            return await userRepository.GetByEmail(email);
        }

        public async Task<string?> Login(string email, string password)
        {
            var hashedPassword = EncryptPassword(password);
            var user = await userRepository.GetByEmail(email);
            if (user != null && user.Password == hashedPassword)
            {
                return CreateToken(user);
            }
            return null;
        }

        public string CreateToken(User user)
        {
            var key = Encoding.ASCII.GetBytes(configuration["JwtKey"] ?? throw new Exception());
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                [
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
                ]),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

		public Task<User?> GetUserByUsername(string username)
		{
			return userRepository.GetByUsername(username);
		}
	}
}
