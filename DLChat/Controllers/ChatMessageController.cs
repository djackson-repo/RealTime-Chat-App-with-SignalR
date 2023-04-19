using DLChat.Services;
using Microsoft.AspNetCore.Mvc;

namespace DLChat.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChatMessageController: ControllerBase
    {
        private readonly ChatMessageServices _chatMessageServices;

        public ChatMessageController(ChatMessageServices chatMessageServices)
        {
            _chatMessageServices = chatMessageServices;
        }
       
        [HttpGet]
        public async Task<IActionResult> GetChatMessage()
        {
            try
            {
                Console.WriteLine("ChatMessageController.GetChatMessage() fetching Chat Messages");

                var chatMessage = await _chatMessageServices.GetAsync();
                if (chatMessage == null) { return NotFound(); }
                return new ObjectResult(chatMessage);

            }
            catch (Exception ex)
            {
                Console.WriteLine("ChatMessageController.GetChatMessage() got error: " + ex.Message + ", Stack = " + ex.StackTrace);
                return StatusCode(500);
            }
        }
    }
}
