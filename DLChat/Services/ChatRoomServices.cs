using DLChat.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace DLChat.Services
{
    public class ChatRoomServices
    {
        private readonly IMongoCollection<ChatRoomModel> _chatRoomCollection;

        public ChatRoomServices(IOptions<DLChatDatabaseSettings> dlChatDatabaseSettings)
        {
            var mongoClient = new MongoClient(dlChatDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(dlChatDatabaseSettings.Value.DatabaseName);

            _chatRoomCollection = mongoDatabase.GetCollection<ChatRoomModel>(dlChatDatabaseSettings.Value.ChatRoomCollectionName);
        }

        public async Task<List<ChatRoomModel>> GetAsync() => await _chatRoomCollection.Find(_ => true).ToListAsync();

        public async Task CreateAsync(ChatRoomModel newRoom) => await _chatRoomCollection.InsertOneAsync(newRoom);

        public async Task<List<ChatRoomModel>> GetUserChatRooms(string userId)
        {
            var result = await _chatRoomCollection.Find(x => x.users.Contains(userId)).ToListAsync();
            return result;
        }
    }
}

