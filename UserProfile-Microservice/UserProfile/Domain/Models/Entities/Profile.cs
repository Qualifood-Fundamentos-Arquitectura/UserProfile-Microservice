using DittoBox.API.UserProfile.Domain.Models.ValueObjects;

namespace DittoBox.API.UserProfile.Domain.Models.Entities
{
	public class Profile
	{
		public int Id { get; set; }
		public int UserId { get; set; }
		public string FirstName { get; set; } = string.Empty;
		public string LastName { get; set; } = string.Empty;
		public int? AccountId { get; set; }
		public int? GroupId { get; set; }
		public ICollection<ProfilePrivilege> ProfilePrivileges { get; set; } = [];
		public Profile()
		{
		}

		public Profile(int userId, string firstName, string lastName)
		{
			UserId = userId;
			FirstName = firstName;
			LastName = lastName;
		}
	}
}