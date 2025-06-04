using System.ComponentModel.DataAnnotations;

namespace DittoBox.API.UserProfile.Application.Commands
{
    public record ChangePasswordCommand(
        [Required] int UserId,
        [Required, StringLength(64, MinimumLength = 8)] string NewPassword
        );
}