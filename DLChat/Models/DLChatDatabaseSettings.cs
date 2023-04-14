namespace DLChat.Models
{
    public class DLChatDatabaseSettings
    {
        public string ConnectionString { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;
        public string UsersCollectionName { get; set; } = null!;
        public string ChatCollectionName { get; set; } = null!;
    }
}
