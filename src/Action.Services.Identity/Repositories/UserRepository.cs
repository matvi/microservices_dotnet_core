using System;
using System.Threading.Tasks;
using Action.Services.Identity.Domain.Models;
using Action.Services.Identity.Domain.Repositories;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Action.Services.Identity.Repositories {
    public class UserRepository : IUserRepository {
        private readonly IMongoDatabase _database;
        public UserRepository (IMongoDatabase database) {
            _database = database;

        }
        public async Task AddAsync (User user)
            => await GetCollection
            .InsertOneAsync(user);
        public async Task<User> GetAsync (Guid id) 
            => await GetCollection
            .AsQueryable()
            .FirstOrDefaultAsync(u => u.Id == id);
        
        public async Task<User> GetAsync (string email) 
            => await GetCollection
            .AsQueryable()
            .FirstOrDefaultAsync(u => u.Email == email);

        public IMongoCollection<User> GetCollection
            => _database.GetCollection<User>("Users");
    }
}