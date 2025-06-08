using DittoBox.API.UserProfile.Application.Commands;
using DittoBox.API.UserProfile.Application.Handlers.Interfaces;
using DittoBox.API.UserProfile.Application.Resources;
using DittoBox.API.UserProfile.Domain.Clients;
using DittoBox.API.UserProfile.Domain.Services.Application;

namespace DittoBox.API.UserProfile.Application.Handlers.Internal
{
	public class LoginCommandHandler(
		IUserService userService,
		IProfileService profileService,
		IAccountServiceClient accountServiceClient
	) : ILoginCommandHandler
	{
		public async Task<LoginResource?> Handle(LoginCommand command)
		{
			var user = await userService.GetUserByEmail(command.Email);
			var token = await userService.Login(command.Email, command.Password);
			if (user == null || token == null)
			{
				return null;
			}

			var profile = await profileService.GetProfile(user.Id);
			var privileges = await profileService.ListUserPrivileges(user.Id);

			int? accountId = null;
			int? groupId = null;

			if (profile != null)
			{
				accountId = profile.AccountId;
				groupId = profile.GroupId;

				if (accountId.HasValue)
				{
					var account = await accountServiceClient.GetAccountById(accountId.Value);
				}
			}

			return new LoginResource()
			{
				UserId = user.Id,
				Username = user.Username,
				Token = token,
				AccountId = accountId,
				GroupId = groupId,
				ProfileId = user.Id,
				Privileges = privileges?.Select(p => p.ToString()).ToArray() ?? []
			};
		}
	}
}
