using System.Collections.Generic;
using System.Threading.Tasks;
using Action.Services.Activities.Domain.Models;
using Action.Services.Activities.Domain.Repositories;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Action.Services.Activities.Repositories
{
    public class CategoryRepository : Action.Services.Activities.Domain.Repositories.ICategoryRepository
    {
        private readonly IMongoDatabase _database;

        public CategoryRepository(IMongoDatabase database)
        {
            _database = database;
        }
        public async Task AddAsync(Category category)
            => await Collection.InsertOneAsync(category);
        

        public async Task<IEnumerable<Category>> BrowseAsync()
            =>  await Collection
                .AsQueryable()
                .ToListAsync();
        

        public async Task<Category> GetAsync(string name)
            => await Collection
                .AsQueryable()
                .FirstOrDefaultAsync(c => c.Name == name.ToLowerInvariant());

        private IMongoCollection<Category> Collection =>
            _database.GetCollection<Category>("Categories");
        
    }
}