namespace DittoBox.API.UserProfile.Domain.Services.Domain
{
	public interface IUserProfileManagementServices
	{
		public Task AssignPrivilegeToUser(int UserId, int PrivilegeId);
		public Task RevokePrivilegeFromUser(int UserId, int PrivilegeId);
		public Task UpdateUserDetails(int UserId, string FirstName, string LastName, string Email);
		public Task UpdateProfileDetails(int UserId, string FirstName, string LastName);
	}
}