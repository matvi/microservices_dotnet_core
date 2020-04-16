using System;

namespace Action.Common.Exceptions
{
    public class ActioException : Exception
    {
        private string v1;
        private string v2;

        public string Code { get; }
        public string ErrorMessage { get; }

        public ActioException(string code)
        {
            Code = code;
        }

        public ActioException(string code, string message)
        {
            Code = code;
            ErrorMessage = message;
        }
    }
}