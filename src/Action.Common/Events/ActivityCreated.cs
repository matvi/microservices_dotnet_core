using System;

namespace Action.Common.Events
{
    public class ActivityCreated : IAuthenticatedEvent
    {
        public Guid UserId { get ;  set;}

        public Guid Id { get;   }
        public string Category { get;   }

        public string Name { get;   }

        public string Description { get;   }
        public DateTime CreatedAt { get;   }

        protected ActivityCreated()
        {
            
        }

        public ActivityCreated(Guid id, Guid userId, string category, string name, string description)
        {
            Id = id;
            UserId = userId;
            Category = category;
            Name = name;
            Description = description;
            CreatedAt = DateTime.Now;
        }
    }
}