using DittoBox.API.Shared.Domain.Repositories;
using DittoBox.API.UserProfile.Domain.Models.Entities;

namespace DittoBox.API.UserProfile.Domain.Repositories {
    public interface IProfileRepository : IBaseRepository<Profile>
    {
        public Task<IEnumerable<Profile>> GetByAccountId(int accountId);
        Task<Profile?> GetById(int id);
    }
}