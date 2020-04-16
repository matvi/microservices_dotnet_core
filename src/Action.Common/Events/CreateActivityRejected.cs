using System;

namespace Action.Common.Events
{
    public class CreateActivityRejected : IRejectedEvent
    {
        public string Reason { get  ; set  ; }
        public string Code { get  ; set  ; }
        public Guid Id {get; }

        protected CreateActivityRejected()
        {}

        public CreateActivityRejected(Guid id, string code, string reason)
        {
            Id = id;
            Code = code;
            Reason = reason;
        }
    }
}