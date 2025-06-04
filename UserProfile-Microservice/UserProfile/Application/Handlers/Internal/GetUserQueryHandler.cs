using DittoBox.API.UserProfile.Application.Resources;
using DittoBox.API.UserProfile.Application.Handlers.Interfaces;
using DittoBox.API.UserProfile.Application.Queries;
using DittoBox.API.UserProfile.Domain.Services.Application;

namespace DittoBox.API.UserProfile.Application.Handlers.Internal
{
    public class GetUserQueryHandler(
        IUserService userService
        ) : IGetUserQueryHandler
    {
        public async Task<UserResource?> Handle(GetUserQuery query)
        {
            var result = await userService.GetUser(query.UserId);
            return result == null ? null : UserResource.FromUser(result);
        }
    }
}
