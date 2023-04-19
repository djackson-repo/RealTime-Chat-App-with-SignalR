using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace DLChat.Models
{
    public class ChatRoomModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string Name { get; set; } = null!;

        public string[] ChatMessages { get; set; } = null!;
        public string[] ChatUsers { get; set; } = null!;
    }
}
