using System.ComponentModel.DataAnnotations;

namespace DittoBox.API.UserProfile.Application.Commands
{
    public record DeleteUserCommand
        (
        [Required] int UserId
        );
}