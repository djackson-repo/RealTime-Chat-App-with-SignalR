namespace DLChat.Models
{
    public class DLChatDatabaseSettings
    {
        public string ConnectionString { get; set; } = "mongodb+srv://djackson:iNeSxBxORsWwvj7M@cluster0.4oootwq.mongodb.net/test";
        public string DatabaseName { get; set; } = "DLChat";
        public string UsersCollectionName { get; set; } = "users";
        public string ChatRoomCollectionName { get; set; } = "chatRoom";

        public static string CognitoClientId { get; } = "6io94psm6vo5nug32gnthfomgp";

        public string ChatMessageCollectionName { get; set; } = "chatMessage";

    }
}