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
        public string UserId { get; set; } = null!;
        public string Message { get; set; } = null!;
 //       public DateTime Timestamp { get; set; }


        
    }
}
