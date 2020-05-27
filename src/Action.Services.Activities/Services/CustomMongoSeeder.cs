using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Action.Common.mongo;
using Action.Services.Activities.Domain.Models;
using Action.Services.Activities.Domain.Repositories;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;

namespace Action.Services.Activities.Services
{
    public class CustomMongoSeeder : MongoSeeder
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ILogger<CustomMongoSeeder> _logger;

        public CustomMongoSeeder(IMongoDatabase database, 
            ICategoryRepository categoryRepository,
            ILogger<CustomMongoSeeder> logger) : base(database)
        {
            _categoryRepository = categoryRepository;
            _logger = logger;
        }

        protected override async Task CustomSeedAsync()
        {
            _logger.LogInformation("***4Sedding categories");
            var categories = new List<string>
            {
                "work",
                "sport",
                "hobby"
            };
            await Task.WhenAll(categories.Select(x =>
                _categoryRepository.AddAsync(new Category(x))));
        }
    }
}