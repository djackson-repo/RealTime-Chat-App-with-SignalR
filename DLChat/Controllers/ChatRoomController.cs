using DLChat.Models;
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
        [HttpPost]
        public async Task<IActionResult> CreateNewRoom(ChatRoomModel newRoom)
        {
            try
            {
                newRoom.Id = null;
                await _chatRoomServices.CreateAsync(newRoom);
                return CreatedAtAction(nameof(GetChatRooms), new { id = newRoom.Id }, newRoom);
            }
            catch (Exception ex)
            {
                Console.WriteLine("ChatRoomController.CreateNewRoom() got error: " + ex.Message + ", Stack = " + ex.StackTrace);
                return StatusCode(500);
            }
        }
        [HttpGet("[action]/{userId}")]
        public async Task<IActionResult> GetUserChatRooms(string userId)
        {
            try
            {
                Console.WriteLine("ChatRoomController.GetUserChatRooms() fetching Chat Rooms");

                var chatRoom = await _chatRoomServices.GetUserChatRooms(userId);
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