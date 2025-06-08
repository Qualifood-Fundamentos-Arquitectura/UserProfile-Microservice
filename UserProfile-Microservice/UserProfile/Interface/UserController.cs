using DittoBox.API.UserProfile.Application.Commands;
using DittoBox.API.UserProfile.Application.Resources;
using DittoBox.API.UserProfile.Application.Handlers.Interfaces;
using DittoBox.API.UserProfile.Application.Queries;
using DittoBox.API.UserProfile.Domain.Clients;
using Microsoft.AspNetCore.Mvc;

namespace DittoBox.API.UserProfile.Interface
{
      /// <summary>
    /// Controller for managing user profiles.
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UserController(
        ILogger<UserController> _logger,
        ICreateUserCommandHandler createUserCommandHandler,
        IGetUserQueryHandler getUserQueryHandler,
        IDeleteUserCommandHandler deleteUserCommandHandler,
        IChangePasswordCommandHandler changePasswordCommandHandler,
        ILoginCommandHandler loginCommandHandler
    ) : ControllerBase
    {
        // <summary>
        /// Gets a user by their identifier.
        /// </summary>
        /// <param name="query">The query object containing the user ID.</param>
        /// <returns>An <see cref="ActionResult{UserResource}"/> containing the user details.</returns>

        [HttpGet("{UserId:int}")]
        public async Task<ActionResult<UserResource>> GetUser([FromRoute] GetUserQuery query)
        {
            try
            {
                var response = await getUserQueryHandler.Handle(query);
                if (response == null)
                {
                    return NotFound();
                }
                return Ok(response);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting user with userId: {userId}", query.UserId);
                return StatusCode(500, "Internal server error");
            }
        }
          /// <summary>
        /// Creates a new user.
        /// </summary>
        /// <param name="user">The command object containing the new user details.</param>
        /// <returns>An <see cref="ActionResult{UserResource}"/> with the created user details.</returns>

        [HttpPost]
        public async Task<ActionResult<UserResource>> CreateUser([FromBody] CreateUserCommand user)
        {
            try
            {
                var response = await createUserCommandHandler.Handle(user);
                _logger.LogInformation("User created with username {username} and id {id}", response.Username, response.Id);
                return CreatedAtAction(nameof(GetUser), new { UserId = response.Id }, response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating user with email {email} and username {username}", user.Email, user.Username);
                return StatusCode(500, "Internal server error");
            }
        }
        /// <summary>
        /// Deletes a user by their identifier.
        /// </summary>
        /// <param name="command">The command object containing the user ID to be deleted.</param>
        /// <returns>An <see cref="ActionResult"/> indicating the result of the operation.</returns>

        [HttpDelete]
        [Route("{UserId:int}")]
        public async Task<ActionResult> DeleteUser([FromRoute] DeleteUserCommand command)
        {
            try
            {
                await deleteUserCommandHandler.Handle(command);
                _logger.LogInformation("User with userId: {userId} deleted", command.UserId);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting user with userId: {userId}", command.UserId);
                return StatusCode(500, "Internal server error");
            }
        }
          /// <summary>
        /// Changes the password of a user.
        /// </summary>
        /// <param name="changePassword">The command object containing the new password details.</param>
        /// <returns>An <see cref="ActionResult"/> indicating the result of the operation.</returns>

        [HttpPut]
        [Route("change-password")]
        public async Task<ActionResult> ChangePassword([FromBody] ChangePasswordCommand changePassword)
        {
            try
            {

                await changePasswordCommandHandler.Handle(changePassword);
                _logger.LogInformation("Password changed for user with id {UserId}", changePassword.UserId);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while changing password for user with id {UserId}", changePassword.UserId);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult> Login([FromBody] LoginCommand login)
        {
            try
            {
                var response = await loginCommandHandler.Handle(login);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while logging in user with email {email}", login.Email);
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
