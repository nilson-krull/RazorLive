namespace RazorStreamTest.Data
{
    using MongoDB.Driver;
    using System.Threading.Tasks;

    public class UserService
    {
        private readonly IMongoCollection<User> _users;

        public UserService()
        {
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("ChatDatabase");
            _users = database.GetCollection<User>("Users");
        }

        public async Task<bool> RegisterUserAsync(string username, string password)
        {
            var existingUser = await _users.Find(u => u.Username == username).FirstOrDefaultAsync();
            if (existingUser != null)
            {
                return false;  // Usuário já existe
            }

            var user = new User
            {
                Username = username,
                Password = password  // Em um sistema real, faça hash da senha!
            };

            await _users.InsertOneAsync(user);
            return true;
        }

        public async Task<User> AuthenticateUserAsync(string username, string password)
        {
            return await _users.Find(u => u.Username == username && u.Password == password).FirstOrDefaultAsync();
        }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            return await _users.Find(u => u.Username == username).FirstOrDefaultAsync();
        }
    }

}
