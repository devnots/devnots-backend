using System.Threading.Tasks;
using DevNots.Application.Contracts;
using DevNots.Application.Contracts.User;
using DevNots.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace DevNots.RestApi.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController: ControllerBase
    {
        private readonly UserService userService;
        public UserController(UserService userService)
        {
            this.userService = userService;
        }
        /// <summary>
        /// Register an User
        /// </summary>
        /// <param name="user">User Object</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> RegisterUser([FromBody] UserDto user)
        {
            var response = await userService.RegisterAsync(user);

            if (response.Error != null)
                return StatusCode(response.Error.StatusCode, response.Error);

            return StatusCode(201, new { id = response.Result });
        }
        /// <summary>
        /// Delete the User
        /// </summary>
        /// <param name="request">DeleteUserDto Object</param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> DeleteUser([FromBody] DeleteUserDto request)
        {
            var response = await userService.DeleteUserAsync(request);

            if (response.Error != null)
                return StatusCode(response.Error.StatusCode, response.Error);

            return Ok(new { message = "User deleted."});
        }
        /// <summary>
        /// Get List of Users
        /// </summary>
        /// <param name="limit">Set the user limit  ( Default limit = 20 )</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetUsers([FromQuery] int limit = 20)
        {
            var response = await userService.GetUsersAsync(new UserListDto()
            {
                Limit = limit,
            });

            if (response.Error != null)
                return StatusCode(response.Error.StatusCode, response.Error.Message);

            return Ok(response.Result);
        }
        /// <summary>
        /// Update the User
        /// </summary>
        /// <param name="request">User Object</param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody] UserDto request)
        {
            var response = await userService.UpdateUserAsync(request);

            if (response.Error != null)
                return StatusCode(response.Error.StatusCode, response.Error);

            var isUpdated = response.Result;

            if (!isUpdated)
                return NotFound(new { message = "404 not found."});

            return Ok(new { message = "user updated." });
        }

    }
}
