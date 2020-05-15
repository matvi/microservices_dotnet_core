using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Action.Api.Domain.Model;

namespace Action.Api.Repositories
{
    public interface IActivityRepository
    {
         Task AddAsync(Activity model);

         Task<Activity> GetAsync(Guid id);

         Task<IEnumerable<Activity>> BrowseAsync(Guid userId);
    }
}