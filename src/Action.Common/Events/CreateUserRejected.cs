namespace Action.Common.Events
{
    public class CreateUserRejected : IRejectedEvent
    {
        public string Reason { get  ; set  ; }
        public string Code { get  ; set  ; }
        public string Email { get; }

        protected CreateUserRejected()
        {
            
        }
        public CreateUserRejected(string reason, string code, string email)
        {
            Reason = reason;
            Code = code;
            Email = email;
        }
    }
}