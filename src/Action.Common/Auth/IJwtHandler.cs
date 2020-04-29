using System;

namespace Action.Common.Auth
{
    public interface IJwtHandler
    {
         JsonWebToken GenerateToken(Guid userId);
    }
}