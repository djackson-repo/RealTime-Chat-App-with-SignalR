using DLChat.Models;
using DLChat.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace DLChat.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserServices _userServices;
        private readonly LoginServices _loginService;

        public UserController(UserServices userService, LoginServices loginService)
        {
            _userServices = userService;
            _loginService = loginService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            try
            {
                Console.WriteLine("UserController.GetUsers() fetching users");

                var user = await _userServices.GetAsync();
                if (user == null) { return NotFound(); }
                return new ObjectResult(user);

            }
            catch (Exception ex)
            {
                Console.WriteLine("UserController.GetUsers() got error: " + ex.Message + ", Stack = " + ex.StackTrace);
                return StatusCode(500);
            }
        }

        [HttpGet("[action]/{username}")]
        public async Task<IActionResult> GetUserByName(string username)
        {
            try
            {
                Console.WriteLine("UserController.GetUsersByName() fetching users");

                List<UserModel> users = await _userServices.GetUserByName(username);
                if (users is null) { return NotFound(); }
                return new ObjectResult(users);

            }
            catch (Exception ex)
            {
                Console.WriteLine("UserController.GetUsers() got error: " + ex.Message + ", Stack = " + ex.StackTrace);
                return StatusCode(500);
            }
        }
        [HttpPost("login")]
        public async Task<ActionResult<LoginModel>> Login(string username, string password)
        {
            try
            {
                await Task.Delay(2000);
                // password: letters, digit, special characters
                // username: letters, digits, @, numbers (normally validate with ZeroBounce, username=email)
                if (username.Length < 4 || username.Length > 18 || password.Length < 8)
                {
                    return BadRequest("Username or password is of invalid length");
                }

                LoginModel response = await _loginService.LoginUser(username, password);
                if (response == null)
                {
                    return BadRequest("User with credentials does not exist.");
                }

                // return user so the client has access to logged user id + username for future requests.
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Can't log in user: An exception occurred -> " + ex.Message);
                Console.WriteLine(ex.StackTrace);
                return BadRequest("Can't log in user: " + ex.Message);
            }
        }
    }
}

