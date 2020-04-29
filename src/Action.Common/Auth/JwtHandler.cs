using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Action.Common.Auth
{
    public class JwtHandler : IJwtHandler
    {

        private readonly JwtOptions _options;

        public JwtHandler(IOptions<JwtOptions> options)
        {
            _options = options.Value;

        }

        // public JsonWebToken GenerateToken(Guid userId)
        // {
        //     var nowUtc = DateTime.UtcNow;
        //     var expires = nowUtc.AddMinutes(_options.ExpireMinutes);
        //     var centuryBegin = new DateTime(1970, 1, 1).ToUniversalTime();
        //     var exp = (long) (new TimeSpan(expires.Ticks - centuryBegin.Ticks).TotalSeconds);
        //     var now = (long) (new TimeSpan(nowUtc.Ticks - centuryBegin.Ticks).TotalSeconds);
        //     var payload = new JwtPayload
        //     {
        //         {"sub", userId},
        //         {"iss", _options.Issuer},
        //         {"iat", now},
        //         {"exp", exp},
        //         {"unique_name", userId}
        //     };
        //     var jwt = new JwtSecurityToken(_jwtHeader, payload);
        //     var token = _jwtSecurityTokenHandler.WriteToken(jwt);

        //     return new JsonWebToken(token, expires);
        // }

        public JsonWebToken GenerateToken(Guid userId)
        {
            var utcNow = DateTime.Now;
            var expires = utcNow.AddMinutes(_options.ExpireMinutes);
            var issuerSiginKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey));
            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey)), SecurityAlgorithms.HmacSha256);

            var claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.UniqueName, userId.ToString()),
                new Claim(JwtRegisteredClaimNames.Iss, _options.Issuer.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, utcNow.ToTimeStamp().ToString(), ClaimValueTypes.Integer64)
            };

            var jwt = new JwtSecurityToken(
                issuer: _options.Issuer,
                claims: claims,
                notBefore: utcNow,
                expires: expires,
                signingCredentials: signingCredentials);

            var token = new JwtSecurityTokenHandler().WriteToken(jwt);

            return new JsonWebToken(token, expires);
        }
    }
}