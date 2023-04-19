using DLChat.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace DLChat.Services
{
    public class ChatMessageServices
    {
        private readonly IMongoCollection<ChatMessageModel> _chatMessageCollection;

        
        public ChatMessageServices(IOptions<DLChatDatabaseSettings> dlChatDatabaseSettings)
        {
            var mongoClient = new MongoClient(dlChatDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(dlChatDatabaseSettings.Value.DatabaseName);

            _chatMessageCollection = mongoDatabase.GetCollection<ChatMessageModel>(dlChatDatabaseSettings.Value.ChatMessageCollectionName);
        }

        public async Task<List<ChatMessageModel>> GetAsync() => await _chatMessageCollection.Find(_ => true).ToListAsync();
    }
}
