using DittoBox.API.UserProfile.Application.Commands;
using DittoBox.API.UserProfile.Application.Handlers.Interfaces;
using DittoBox.API.UserProfile.Application.Queries;
using DittoBox.API.UserProfile.Application.Resources;
using DittoBox.API.UserProfile.Domain.Models.ValueObjects;
using Microsoft.AspNetCore.Mvc;

namespace DittoBox.API.UserProfile.Interface
{
    /// <summary>
    /// Controller for managing user profiles.
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ProfileController(
        ILogger<ProfileController> _logger,
        IGetProfileDetailsQueryHandler getProfileDetailsQueryHandler,
        IUpdateProfileNamesCommandHandler updateProfileNamesCommandHandler,
        IGrantPrivilegeCommandHandler grantPrivilegeCommandHandler,
        IRevokePrivilegeCommandHandler revokePrivilegeCommandHandler
    ) : ControllerBase
    {
        /// <summary>
        /// Gets the details of a profile by its identifier.
        /// </summary>
        /// <param name="query">The query object containing the profile ID.</param>
        /// <returns>An <see cref="ActionResult{ProfileResource}"/> containing the profile details.</returns>
        [HttpGet("{ProfileId:int}")]
        public async Task<ActionResult<ProfileResource>> GetProfileDetails([FromRoute] GetProfileQuery query)
        {
            try
            {
                var result = await getProfileDetailsQueryHandler.Handle(query);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting profile with profileId: {profileId}", query.ProfileId);
                return StatusCode(500, "Internal server error");
            }
        }
            /// <summary>
        /// Updates the names of a profile.
        /// </summary>
        /// <param name="command">The command object containing the updated profile names.</param>
        /// <returns>An <see cref="ActionResult"/> indicating the result of the operation.</returns>

        [HttpPut]
        [Route("update-names")]
        public async Task<ActionResult> UpdateProfileNames([FromBody] UpdateProfileNamesCommand command)
        {
            try
            {
                await updateProfileNamesCommandHandler.Handle(command);
                _logger.LogInformation("Profile names updated with profileId: {profileId}", command.ProfileId);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating profile names with profileId: {profileId}", command.ProfileId);
                return StatusCode(500, "Internal server error");
            }
        }
        /// <summary>
        /// Grants privileges to a profile.
        /// </summary>
        /// <param name="privilege">The command object containing the privilege details.</param>
        /// <returns>An <see cref="ActionResult"/> indicating the result of the operation.</returns>

        [HttpPost]
        [Route("grant-privileges")]
        public async Task<ActionResult> GrantPrivilege([FromBody] GrantPrivilegeCommand privilege)
        {
            try
            {
                await grantPrivilegeCommandHandler.Handle(privilege);
                _logger.LogInformation("Privileges granted with profileId: {profileId}", privilege.ProfileId);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while granting privileges with profileId: {profileId}", privilege.ProfileId);
                return StatusCode(500, "Internal server error");
            }
        }
         /// <summary>
        /// Revokes privileges from a profile.
        /// </summary>
        /// <param name="privilege">The command object containing the privilege details.</param>
        /// <returns>An <see cref="ActionResult"/> indicating the result of the operation.</returns>

        [HttpPut]
        [Route("revoke-privileges")]
        public async Task<ActionResult> RevokePrivilege([FromBody] RevokePrivilegeCommand privilege)
        {
            try
            {
                await revokePrivilegeCommandHandler.Handle(privilege);
                _logger.LogInformation("Privileges revoked with profileId: {profileId}", privilege.ProfileId);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while revoking privileges with profileId: {profileId}", privilege.ProfileId);
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
