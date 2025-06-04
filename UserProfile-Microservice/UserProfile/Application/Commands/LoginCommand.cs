using System.ComponentModel.DataAnnotations;

namespace DittoBox.API.UserProfile.Application.Commands
{
    public record LoginCommand(
        [Required, EmailAddress] string Email,
        [Required] string Password
    );
}
