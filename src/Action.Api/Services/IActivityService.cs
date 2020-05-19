using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Action.Api.Domain.Model;

namespace Action.Api.Services
{
    public interface IActivityService
    {
         Task<IEnumerable<Activity>> GetActivities(Guid userId);

         Task<Activity> GetActivityById(Guid activityId);
    }
}