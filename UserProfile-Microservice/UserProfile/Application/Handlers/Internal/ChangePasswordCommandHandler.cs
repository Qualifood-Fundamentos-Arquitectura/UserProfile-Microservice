using DittoBox.API.Shared.Domain.Repositories;
using DittoBox.API.UserProfile.Application.Commands;
using DittoBox.API.UserProfile.Application.Handlers.Interfaces;
using DittoBox.API.UserProfile.Domain.Services.Application;

namespace DittoBox.API.UserProfile.Application.Handlers.Internal
{
    public class ChangePasswordCommandHandler(
        IUserService userService,
        IUnitOfWork unitOfWork
        ) : IChangePasswordCommandHandler
    {
        public async Task Handle(ChangePasswordCommand command)
        {
            var user = await userService.GetUser(command.UserId);
            user!.Password = userService.EncryptPassword(command.NewPassword);
            await userService.UpdateUser(user);
            await unitOfWork.CompleteAsync();
        }
    }
}
