using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Action.Api.Domain.Model;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Action.Api.Repositories
{
    public class ActivityRepository : IActivityRepository
    {
        private readonly IMongoDatabase _database;

        public ActivityRepository(IMongoDatabase database)
        {
            _database = database;
        }

        public async Task AddAsync(Activity model)
            => await Collection.InsertOneAsync(model);

        public async Task<IEnumerable<Activity>> BrowseAsync(Guid userId)
            => await Collection
                .AsQueryable()
                .Where(a => a.UserId == userId)
                .ToListAsync();

        public async Task<Activity> GetAsync(Guid id)
            => await Collection
            .AsQueryable()
            .FirstOrDefaultAsync(a => a.Id == id);


        private IMongoCollection<Activity> Collection
            => _database.GetCollection<Activity>("Activity");
    }
}