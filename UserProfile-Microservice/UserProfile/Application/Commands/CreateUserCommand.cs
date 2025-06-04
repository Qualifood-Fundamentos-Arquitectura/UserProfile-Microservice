using System.ComponentModel.DataAnnotations;

namespace DittoBox.API.UserProfile.Application.Commands
{
	public record CreateUserCommand(
		[Required, StringLength(64)] string FirstName,
		[Required, StringLength(64)] string LastName,
		[Required, StringLength(64)] string Username,
		[Required, StringLength(64), EmailAddress] string Email,
		[Required, StringLength(64, MinimumLength = 8)] string Password
	);
}