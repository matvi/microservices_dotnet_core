using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Action.Api.Domain.Model;
using Action.Api.Repositories;

namespace Action.Api.Services {
    public class ActivityService : IActivityService {
        private readonly IActivityRepository _activityRepository;
        public ActivityService (IActivityRepository activityRepository) 
        {
            _activityRepository = activityRepository;
        }
        public async Task<IEnumerable<Activity>> GetActivities (Guid userId) 
        {
           return await _activityRepository.BrowseAsync(userId);
        }

        public async Task<Activity> GetActivityById(Guid activityId)
        {
            return await _activityRepository.GetAsync(activityId);
        }
    }
}