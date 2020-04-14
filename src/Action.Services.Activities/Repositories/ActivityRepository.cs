using System;
using System.Threading.Tasks;
using Action.Services.Activities.Domain.Models;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Action.Services.Activities.Repositories
{
    public class ActivityRepository : Action.Services.Activities.Domain.Repositories.IActivityRepository
    {
        private readonly IMongoDatabase _database;

        public ActivityRepository(IMongoDatabase database)
        {
            _database = database;
        }

        public async Task AddAsync(Activity activity)
            => await Collection.InsertOneAsync(activity);

        public async Task<Activity> GetAsync(Guid id)
            => await Collection
                .AsQueryable()
                .FirstOrDefaultAsync(a => a.Id == id);

        private IMongoCollection<Activity> Collection
            => _database.GetCollection<Activity>("Activity");
    }
}