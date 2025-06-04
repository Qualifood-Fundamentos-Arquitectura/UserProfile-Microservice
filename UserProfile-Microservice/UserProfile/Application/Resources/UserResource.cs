using DittoBox.API.UserProfile.Domain.Models.Entities;

namespace DittoBox.API.UserProfile.Application.Resources
{
	public record UserResource(
		int Id,
		string Username,
		string Email
	)
	{
		public static UserResource FromUser(User user)
        {
            return new UserResource(user.Id, user.Username, user.Email);
        }
    }
}