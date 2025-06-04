using System.ComponentModel.DataAnnotations;

namespace DittoBox.API.UserProfile.Application.Resources
{
    public record LoginResource
    {
        public int UserId { get; init; }
        public string Username { get; init; } = string.Empty;
        public string Token { get; init; } = string.Empty;
        public int? AccountId { get; init; }
        public int? GroupId { get; init; }
        public int ProfileId { get; init; }
		public string[] Privileges { get; init; } = [];
    }
}
