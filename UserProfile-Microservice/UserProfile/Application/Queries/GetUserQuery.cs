using System.ComponentModel.DataAnnotations;

namespace DittoBox.API.UserProfile.Application.Queries
{
    public record GetUserQuery (
        [Required] int UserId
    );
}