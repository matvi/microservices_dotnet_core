namespace Action.Common.Events
{
    public class UserCreated : IEvent
    {
        public string Email { get; }

        public string UserName { get; }

        public UserCreated(string email, string name)
        {
            Email = email;
            UserName = name;
        }

        //we create this so our serializer don´t have any issues when trying to serialize this message
        protected UserCreated()
        {

        }
    }
}