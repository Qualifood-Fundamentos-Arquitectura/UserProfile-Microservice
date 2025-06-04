using System.ComponentModel.DataAnnotations;

namespace DittoBox.API.UserProfile.Application.Queries
{
    public record GetProfileQuery(
        [Required, Range(1, int.MaxValue)] int ProfileId
    );
}