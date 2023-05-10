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
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return null;
            }
        }


        public async Task<UserModel?> GetAsync(string id)
        {
            try
            {
                return await _userCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return null;
            }
        }

        public async Task<UserModel> FindUserAsync(string name, string password)
        {
            try
            {
                // Testing if credentials are valid 
                if (String.IsNullOrWhiteSpace(name) || String.IsNullOrWhiteSpace(password))
                {
                    Console.WriteLine("Invalid credentials: Username and/or password cannot be empty.");
                    await Task.Delay(2000);
                    return null;
                }
                if (name.Length < 4 || name.Length > 18 || password.Length < 8 || password.Length > 18)
                {
                    Console.WriteLine("Invalid credentials: Username and/or password doesn't meet length requirements. (username: [4, 18], password: [8, 18])");
                    await Task.Delay(2000);
                    return null;
                }

                // Checking if credentials exist in database
                var projection = Builders<UserModel>.Projection.Include(u => u.name).Include(u => u.password);
                var user = await _userCollection.Find(u => u.name == name).Project<UserModel>(projection).FirstOrDefaultAsync();

                if (user != null && password.Equals(user.password))
                {
                    return user;
                }
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return null;
            }
        }

        public async Task<UserModel> FindUserNameAsync(string username)
        {
            try
            {
                if (username.Length < 4 || username.Length > 18 || String.IsNullOrWhiteSpace(username))
                {
                    Console.WriteLine("Username is not valid: Doesn't meet length requirements or is empty.");
                    await Task.Delay(2000);
                    return null;
                }

                var projection = Builders<UserModel>.Projection.Include(u => u.name);
                var user = await _userCollection.Find(u => u.name == username).Project<UserModel>(projection).FirstOrDefaultAsync();

                if (user != null)
                {
                    return user;
                }
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return null;
            }
        }
        public async Task UpdateAsync(string id, UserModel updatedUser)
        {
            try
            {
                await _userCollection.ReplaceOneAsync(x => x.Id == id, updatedUser);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
        }


        public async Task RemoveAsync(string id)
        {
            try
            {
                await _userCollection.DeleteOneAsync(x => x.Id == id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
        }
    }
}