using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Linked_backend.Data;
using Linked_backend.Models;
using Microsoft.EntityFrameworkCore;
using Linked_backend.Services;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Linked_backend.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly ScheduleContext _context;
        private IUserService _userService;

        public UserController(ScheduleContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }

        // GET api/users
        [HttpGet]
        [Authorize]
        public ActionResult<IEnumerable<User>> Get()
        {
            var users = _context.Users.OrderBy(m => m.Naam);
            return users
                .Include(l => l.Role)
                .ToList();
        }

        private User GetUser(int id)
        {
            return _context.Users.FirstOrDefault(a => a.UserID == id);
        }

        // GET api/users/5
        [HttpGet("{id}")]
        [Authorize]
        public ActionResult<User> Get(int id)
        {
            return GetUser(id);
        }

        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] User gebruikerParam)
        {
            var user = _userService.Authenticate(gebruikerParam.Email, gebruikerParam.Paswoord);

            if (user == null)
                return BadRequest(new { message = "Email or password is incorrect" });

            return Ok(user);
        }

        // POST api/users/
        [HttpPost]
        [Authorize]
        public ActionResult<User> Post([FromBody] User value)
        {
            value.Paswoord = BCrypt.Net.BCrypt.HashPassword(value.Paswoord);
            _context.Users.Add(value);
            _context.SaveChanges();
            return GetUser(value.UserID);
        }

        // PUT api/users/5
        [HttpPut("{id}")]
        [Authorize]
        public ActionResult<User> Put(int id, [FromBody] User value)
        {
            var user = _context.Users.SingleOrDefault(a => a.UserID == id);
            user.Naam = value.Naam;
            user.Email = value.Email;
            user.Paswoord = BCrypt.Net.BCrypt.HashPassword(value.Paswoord);
            user.Role = value.Role;
            user.RoleID = value.RoleID;
            _context.SaveChanges();
            return GetUser(user.UserID);
        }

        // DELETE api/users/5
        [HttpDelete("{id}")]
        [Authorize]
        public ActionResult<User> Delete(int id)
        {
            var user = _context.Users.SingleOrDefault(a => a.UserID == id);
            _context.Users.Remove(user);
            _context.SaveChanges();
            return user;
        }
    }
}
