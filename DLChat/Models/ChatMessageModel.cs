using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using Microsoft.AspNetCore.SignalR;

namespace DLChat.Models
{
    public class ChatMessageModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string userId { get; set; } = null!;
        public string chatRoomId { get; set; } = null!;
        public string message { get; set; } = null!;
 //       public DateTime Timestamp { get; set; }


        
    }
}
