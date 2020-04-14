using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;

namespace Action.Common.mongo
{
    public class MongoDatabaseInitilizer : IDatabaseInitializer
    {
        private bool _initialized;
        private readonly bool _seed;
        private readonly IMongoDatabase _database;
        private readonly IDatabaseSeeder _databaseSeeder;

        public MongoDatabaseInitilizer(IMongoDatabase database, IOptions<MongoOptions> options, IDatabaseSeeder databaseSeeder)
        {
            _databaseSeeder = databaseSeeder;
            _database = database;
            _seed = options.Value.Seed;
        }
        public async Task InitializeAsync()
        {
            if (_initialized) return;

            RegisterConventions();
            _initialized = true;

            if (!_seed) return;

            await _databaseSeeder.SeedAsync();

        }

        private void RegisterConventions()
        {
            ConventionRegistry.Register("ActionConventions", new MongoConvention(), x => true);
        }

        private class MongoConvention : IConventionPack
        {
            public IEnumerable<IConvention> Conventions => new List<IConvention>
            {
                new IgnoreExtraElementsConvention(true),
                new EnumRepresentationConvention(BsonType.String),
                new CamelCaseElementNameConvention()
            };
        }
    }
}