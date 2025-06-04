using DittoBox.API.UserProfile.Domain.Models.Entities;

namespace DittoBox.API.UserProfile.Domain.Models.ValueObjects
{
    public class ProfilePrivilege
    {
        public int Id { get; set; }
        public int ProfileId { get; set; }
        public Profile Profile { get; set; } = null!;
        public Privilege Privilege { get; set; } 

        public ProfilePrivilege()
        {
        }

        public ProfilePrivilege(int profileId, Privilege privilege)
        {
            ProfileId = profileId;
            Privilege = privilege;
        }
    }
}
