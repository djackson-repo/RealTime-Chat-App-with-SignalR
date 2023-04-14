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
        public async Task<List<UserModel>> Get() =>
            await _userServices.GetAsync();
/*        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<UserModel>> Get(string id)
        {
            var user = await _userServices.GetAsync(id);

            if (user is null)
            {
                return NotFound();
            }

            return user;
        }*/
    }
}
