using DLChat.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace DLChat.Services
{
    public class UserServices
    {
        private readonly IMongoCollection<UserModel> _userCollection;
        
        public UserServices(IOptions<DLChatDatabaseSettings> dlChatDatabaseSettings)
        {
            var mongoClient = new MongoClient(dlChatDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(dlChatDatabaseSettings.Value.DatabaseName);

            _userCollection = mongoDatabase.GetCollection<UserModel>(dlChatDatabaseSettings.Value.UsersCollectionName);
        }

        public async Task<List<UserModel>> GetAsync() => await _userCollection.Find(_ => true).ToListAsync();


    }
}
