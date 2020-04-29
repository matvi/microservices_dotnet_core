using System;

namespace Action.Common.Auth
{
    public class JsonWebToken
    {
        public string Token { get; set; }
        public DateTime Expires { get; set; }

        public JsonWebToken(string token, DateTime expires)
        {
            Token = token;
            Expires = expires;
        }
    }
}