using DLChat.Models;
using DLChat.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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

        [HttpPost]
        public async Task<IActionResult> GetUsersByName(string username)
        {
            try
            {
                Console.WriteLine("UserController.GetUsersByName() fetching users");

                var user = await _userServices.GetUserByName(username);
                if (user == null) { return NotFound(); }
                List<string> usernames = new List<string>();
                for (int i = 0; i < user.Count && user != null; i++)
                {
                    usernames.Add(user[i].name);
                }
                return new ObjectResult(usernames);

            }
            catch (Exception ex)
            {
                Console.WriteLine("UserController.GetUsers() got error: " + ex.Message + ", Stack = " + ex.StackTrace);
                return StatusCode(500);
            }
        }


        /*[HttpPost("[action]")]
        public IActionResult Post([FromBody] UserModel value)
        {
            try
            {
                Console.WriteLine("UserController.Post() posting a new item");

                    if (String.IsNullOrWhiteSpace(value.name))
                    {
                        return BadRequest("Missing username");
                    }
                    if (String.IsNullOrWhiteSpace(value.password))
                    {
                        return BadRequest("Missing password");
                    }
                    if (db.User.FirstOrDefault(x => x.Name == value.name && x.Password == value.password) == null)
                    {
                        return NotFound("Invalid username or password");
                    }

                    return new OkResult();
                

            }
            catch (Exception ex)
            {
                Console.WriteLine("CustomerController.Post() got error: " + ex.Message + ", Stack = " + ex.StackTrace);
                return StatusCode(500);
            }
        }*/


    }
}

