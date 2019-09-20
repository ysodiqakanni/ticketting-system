using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using TickettingSystem.Data.Contracts;
using TickettingSystem.Data.DbModel;
using TickettingSystem.Services.Contracts;
using System.Linq;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace TickettingSystem.Services.Implementations
{
    public class StaffService: IStaffService
    {
        private readonly IUnitOfWork uow;
        private readonly AppSettings _appSettings;
        public StaffService(IUnitOfWork _uow, IOptions<AppSettings> appSettings)
        {
            uow = _uow;
            _appSettings = appSettings.Value;
        }
        private List<StaffDetails> _users = new List<StaffDetails>
        {
            new StaffDetails { Id = 1, Firstname = "Test", Surname = "User", Staffuserid = "test", PasswordHash = "test" }
        };
         

        public StaffDetails Authenticate(string username, string password, out string accessToken)
        {
            accessToken = "";
            var user = _users.SingleOrDefault(x => x.Staffuserid == username && x.PasswordHash == password);

            // return null if user not found
            if (user == null)
                return null;

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                //Subject = new ClaimsIdentity(new Claim[]
                //{
                //    new Claim(ClaimTypes.Name, user.Id.ToString())
                //}),
               Subject = new ClaimsIdentity(GetUserClaims(user)),

                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            accessToken = tokenHandler.WriteToken(token);

            // remove password before returning
           // user.PasswordHash = null;

            return user;
        }
        private IEnumerable<Claim> GetUserClaims(StaffDetails user)
        {
            var claims = new Claim[]
            {
            new Claim(ClaimTypes.Name, user.Id.ToString()),
            new Claim("USERID", user.Staffuserid) ,
            //new Claim("ACCESS_LEVEL", user.ACCESS_LEVEL?.ToUpper()),
            //new Claim("READ_ONLY", user.READ_ONLY?.ToUpper())
            };
            return claims;
        }

        public IEnumerable<StaffDetails> GetAll()
        {
            // return users without passwords
            return _users.Select(x => {
                x.PasswordHash = null;
                return x;
            });
        }
    }
}
