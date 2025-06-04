using DittoBox.API.Shared.Domain.Repositories;
using DittoBox.API.UserProfile.Application.Commands;
using DittoBox.API.UserProfile.Application.Handlers.Interfaces;
using DittoBox.API.UserProfile.Application.Resources;
using DittoBox.API.UserProfile.Domain.Services.Application;

namespace DittoBox.API.UserProfile.Application.Handlers.Internal
{
	public class CreateUserCommandHandler(
		IUserService userService,
		IProfileService profileService,
        IUnitOfWork unitOfWork
        ) : ICreateUserCommandHandler
	{
		public async Task<UserResource> Handle(CreateUserCommand command)
		{
			var existingUser = await userService.GetUserByEmail(command.Email);
			if (existingUser != null)
			{
				throw new Exception("Email already in use");
			}

			existingUser = await userService.GetUserByUsername(command.Username);
			if (existingUser != null)
			{
				throw new Exception("Username already in use");
			}

			var result = await userService.CreateUser(command.Username, command.Email, command.Password);
            await unitOfWork.CompleteAsync();
            _ = await profileService.CreateProfile(result.Id, command.FirstName, command.LastName);
            await unitOfWork.CompleteAsync();
            return UserResource.FromUser(result);
        }
	}
}