using System.Net.Http.Json;
using DittoBox.API.UserProfile.Domain.Clients;

namespace DittoBox.API.UserProfile.Infrastructure.Clients
{
    public class AccountServiceClient(HttpClient httpClient, IConfiguration config) : IAccountServiceClient
    {
        public async Task<AccountDto?> GetAccountById(int accountId)
        {
            var baseUrl = config["Services:AccountService"];
            var response = await httpClient.GetAsync($"{baseUrl}/account/{accountId}");

            if (!response.IsSuccessStatusCode)
                return null;

            return await response.Content.ReadFromJsonAsync<AccountDto>();
        }
    }

    public class AccountDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
    }
}