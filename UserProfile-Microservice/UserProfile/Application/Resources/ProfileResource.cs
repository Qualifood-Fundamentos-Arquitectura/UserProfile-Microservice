using DittoBox.API.UserProfile.Domain.Models.Entities;

namespace DittoBox.API.UserProfile.Application.Resources
{
	public record ProfileResource
	{
		public int Id { get; set; }
		public string FirstName { get; set; } = string.Empty;
		public string LastName { get; set; } = string.Empty;
		public int? AccountId { get; set; }
		public int? GroupId { get; set; }
		public int UserId { get; set; }
		public string[] Privileges { get; init; } = [];


		public static ProfileResource FromProfile(Profile profile)
		{
			return new ProfileResource()
			{
				Id = profile.Id,
				FirstName = profile.FirstName,
				LastName = profile.LastName,
				AccountId = profile.AccountId,
				GroupId = profile.GroupId,
				UserId = profile.UserId,
				Privileges = profile.ProfilePrivileges != null
					? profile.ProfilePrivileges.Select(p => p.Privilege.ToString()).ToArray()
					: []
			};
		}
	}
}