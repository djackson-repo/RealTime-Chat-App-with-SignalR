using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace DLChat.Models
{
    public class UserModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string name { get; set; } = null!;

        public string password { get; set; } = null!;

        public string? token { get; set; }
    }
}
