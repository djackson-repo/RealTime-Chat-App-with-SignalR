namespace DLChat.Models
{
    public class DLChatDatabaseSettings
    {
        public string ConnectionString { get; set; } = "mongodb+srv://djackson:FRFAFBfdy0ePu0Yv@cluster0.4oootwq.mongodb.net/test";
        public string DatabaseName { get; set; } = "DLChat";
        public string UsersCollectionName { get; set; } = "users";
        public string ChatCollectionName { get; set; } = "chat";
    }
}
