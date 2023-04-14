using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace DLChat.Models
{
    public class UserModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string Name { get; set; } = null!;

        public string Password { get; set; } = null!;
    }
}
