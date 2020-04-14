using System;
using System.Threading.Tasks;
using Action.Services.Activities.Domain.Models;

namespace Action.Services.Activities.Domain.Repositories
{
    public interface IActivityRepository
    {
         Task<Activity> GetAsync(Guid id);
         Task AddAsync(Activity activity);
    }
}