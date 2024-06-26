﻿using DLChat.Models;
using DLChat.Services;
using Microsoft.AspNetCore.Mvc;

namespace DLChat.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChatMessageController : ControllerBase
    {
        private readonly ChatMessageServices _chatMessageServices;

        public ChatMessageController(ChatMessageServices chatMessageServices)
        {
            _chatMessageServices = chatMessageServices;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var chatMessage = await _chatMessageServices.GetAsync();
                if (chatMessage == null) { return NotFound(); }
                return new ObjectResult(chatMessage);
            }
            catch (Exception ex)
            {
                Console.WriteLine("ChatMessageController.Get() got error: " + ex.Message + ", Stack = " + ex.StackTrace);
                return StatusCode(500);
            }

        }



        [HttpPost("[action]")]
        public async Task<IActionResult> GetChatMessages([FromBody] ChatMessageModel roomInfo)
        {
            try
            {
                Console.WriteLine("ChatMessageController.GetChatMessage() fetching Chat Messages");

                var chatMessage = await _chatMessageServices.GetMessages(roomInfo.chatRoomId, roomInfo.userId);
                if (chatMessage == null) { return NotFound(); }
                return new ObjectResult(chatMessage);

            }
            catch (Exception ex)
            {
                Console.WriteLine("ChatMessageController.GetChatMessage() got error: " + ex.Message + ", Stack = " + ex.StackTrace);
                return StatusCode(500);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(ChatMessageModel newMessage)
        {
            try
            {
                newMessage.Id = null;
                await _chatMessageServices.CreateMessage(newMessage);
                return CreatedAtAction(nameof(Get), new { id = newMessage.Id }, newMessage);
            }
            catch (Exception ex)
            {
                Console.WriteLine("ChatMessageController.PostMess+age() got error: " + ex.Message + ", Stack = " + ex.StackTrace);
                return StatusCode(500);
            }
        }
    }
}
