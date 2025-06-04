using DittoBox.API.UserProfile.Application.Queries;
using DittoBox.API.UserProfile.Application.Resources;

namespace DittoBox.API.UserProfile.Application.Handlers.Interfaces
{
    public interface IGetUserQueryHandler
    {
        public Task<UserResource?> Handle(GetUserQuery query);
    }
}