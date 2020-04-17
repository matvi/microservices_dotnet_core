using System;
using System.Threading.Tasks;
using Action.Common.Exceptions;
using Action.Services.Activities.Domain.Models;
using Action.Services.Activities.Domain.Repositories;

namespace Action.Services.Activities.Services
{
    public class ActivityService : IActivityService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IActivityRepository _activityRepository;
        public ActivityService (IActivityRepository activityRepository, ICategoryRepository categoryRepository)
        {
            _activityRepository = activityRepository;
            _categoryRepository = categoryRepository;

        }
        public async Task AddAsync (Guid id, Guid userId, string category, string name, string description, DateTime createdAt) 
        {
            var activityCategory = await _categoryRepository.GetAsync(category);
            if (activityCategory == null)
            {
                throw new ActioException("category_not_found", $"Category: {category} not found.");
            }
            var activity = new Activity(id,activityCategory,userId,name,description,createdAt);
            await _activityRepository.AddAsync(activity);

        }

    
    }
}