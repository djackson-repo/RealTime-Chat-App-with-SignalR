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

        public async Task<List<ChatRoomModel>> GetAsync()
        {
            try
            {
                return await _chatRoomCollection.Find(_ => true).ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return null;
            }

        }

        public async Task CreateAsync(ChatRoomModel newRoom)
        {
            try
            {
                await _chatRoomCollection.InsertOneAsync(newRoom);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }

        }

        public async Task<List<ChatRoomModel>> GetUserChatRooms(string userId)
        {
            try
            {
                return await _chatRoomCollection.Find(x => x.users.Contains(userId)).ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return null;
            }
        }

        public async Task<ChatRoomModel> GetRoomInfo(string chatRoomId)
        {
            try
            {
                var result = await _chatRoomCollection.Find(x => x.Id == chatRoomId).FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return null;
            }

        }

        public async Task UpdateAsync(string id, ChatRoomModel updatedRoom)
        {
            try
            {
                await _chatRoomCollection.ReplaceOneAsync(x => x.Id == id, updatedRoom);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }

        }
       
    }
}

