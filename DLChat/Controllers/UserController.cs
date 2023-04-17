using DLChat.Models;
using DLChat.Services;
using Microsoft.AspNetCore.Mvc;

namespace DLChat.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserServices _userServices;

        public UserController(UserServices userServices) =>
            _userServices = userServices;
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


    }
}

