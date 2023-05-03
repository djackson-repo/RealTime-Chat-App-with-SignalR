using DLChat.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace DLChat.Services
{
    public class UserServices
    {
        private readonly IMongoCollection<UserModel>? _userCollection;

        public UserServices(IOptions<DLChatDatabaseSettings> dlChatDatabaseSettings)
        {
            try
            {
                var mongoClient = new MongoClient(dlChatDatabaseSettings.Value.ConnectionString);

                var mongoDatabase = mongoClient.GetDatabase(dlChatDatabaseSettings.Value.DatabaseName);

                _userCollection = mongoDatabase.GetCollection<UserModel>(dlChatDatabaseSettings.Value.UsersCollectionName);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }

        }

        public async Task<List<UserModel>> GetAsync() => await _userCollection.Find(_ => true).ToListAsync();

        public async Task<List<UserModel>> GetUserByName(string username)
        {
            try
            {
                return await _userCollection.Find(x => x.name.Contains(username)).ToListAsync();
            }
             catch(Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return null;
            }
    // await _userCollection.Find(x => x.name.Contains(username)).ToListAsync();
}

    }
}
