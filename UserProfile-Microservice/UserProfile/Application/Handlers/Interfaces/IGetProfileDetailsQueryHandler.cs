using DittoBox.API.UserProfile.Application.Queries;
using DittoBox.API.UserProfile.Application.Resources;

namespace DittoBox.API.UserProfile.Application.Handlers.Interfaces
{
    public interface IGetProfileDetailsQueryHandler
    {
        public Task<ProfileResource?> Handle(GetProfileQuery query);
    }
}
