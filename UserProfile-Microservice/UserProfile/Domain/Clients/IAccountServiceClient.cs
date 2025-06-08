using DittoBox.API.UserProfile.Infrastructure.Clients;

namespace DittoBox.API.UserProfile.Domain.Clients
{
    public interface IAccountServiceClient
    {
        Task<AccountDto?> GetAccountById(int accountId);
    }
}