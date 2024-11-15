using MongoDB.Driver;
using RazorStreamTest.Data.Models;

namespace RazorStreamTest.Data
{
    public class ChatService
    {
        private readonly IMongoCollection<ChatMessage> _chatMessages;

        public ChatService()
        {
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("ChatDatabase");
            _chatMessages = database.GetCollection<ChatMessage>("Messages");
        }

        public async Task SaveMessageAsync(ChatMessage message)
        {
            await _chatMessages.InsertOneAsync(message);
        }

        public async Task<List<ChatMessage>> GetMessagesAsync()
        {
            return await _chatMessages.Find(_ => true).ToListAsync();
        }
    }
}
