using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace DLChat.Models
{
    public class ChatRoomModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string[] users { get; set; } = null!;
        public string[] admins { get; set; } = null!;
        public string chatName { get; set; } = null!;
    }
}
