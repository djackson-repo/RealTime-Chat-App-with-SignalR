﻿using DLChat.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Collections;

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

        public async Task<List<ChatMessageModel>> GetMessages(string chatRoomId, string userId)
        {
            try
            {
                var result = await _chatMessageCollection.Find(x => x.chatRoomId == chatRoomId && x.userId == userId).ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return null;
            }
        }
        public async Task CreateMessage(ChatMessageModel newMessage)
        {
            try
            {
                await _chatMessageCollection.InsertOneAsync(newMessage);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
        }

    }
}
