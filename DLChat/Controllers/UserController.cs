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

        public UserController(UserServices userServices)
        {
            _userServices = userServices;
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

    }
}

