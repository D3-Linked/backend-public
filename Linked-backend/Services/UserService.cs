using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Linked_backend.Data;
using Linked_backend.Helpers;
using Linked_backend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Linked_backend.Services
{
    public class UserService : IUserService
    {
        private readonly AppSettings _appSettings;
        private readonly ScheduleContext _scheduleContext;

        public UserService(IOptions<AppSettings> appSettings, ScheduleContext scheduleContext)
        {
            _appSettings = appSettings.Value;
            _scheduleContext = scheduleContext;
        }

        public User Authenticate(string email, string paswoord)
        {

            var user = _scheduleContext.Users.Include(r => r.Role).SingleOrDefault(x => x.Email == email);
            if (user == null)
                return null;

            bool isValid = BCrypt.Net.BCrypt.Verify(paswoord, user.Paswoord);

            if (isValid)
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                    new Claim("UserID", user.UserID.ToString()),
                    new Claim("Email", user.Email),
                    new Claim("RoleID", user.RoleID.ToString()),
                    new Claim("Rol", user.Role.Naam)
                    }),
                    Expires = DateTime.UtcNow.AddDays(7),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                user.Token = tokenHandler.WriteToken(token);

                // remove password before returning
                user.Paswoord = null;

                return user;
            }
            return null;
        }
    }
}
