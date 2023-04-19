using DLChat.Services;
using Microsoft.AspNetCore.Mvc;

namespace DLChat.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChatRoomController : ControllerBase
    {
        private readonly ChatRoomServices _chatRoomServices;

        public ChatRoomController(ChatRoomServices chatRoomServices)
        {
            _chatRoomServices = chatRoomServices;
        }
        [HttpGet]
        public async Task<IActionResult> GetChatRooms()
        {
            try
            {
                Console.WriteLine("ChatRoomController.GetChatRooms() fetching Chat Rooms");

                var chatRoom = await _chatRoomServices.GetAsync();
                if (chatRoom == null) { return NotFound(); }
                return new ObjectResult(chatRoom);

            }
            catch (Exception ex)
            {
                Console.WriteLine("ChatRoomController.GetChatRooms() got error: " + ex.Message + ", Stack = " + ex.StackTrace);
                return StatusCode(500);
            }
        }


    }
}