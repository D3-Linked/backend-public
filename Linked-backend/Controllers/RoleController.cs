using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Linked_backend.Data;
using Linked_backend.Models;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Linked_backend.Controllers
{
    [Route("api/roles")]
    [ApiController]
    public class RoleController : Controller
    {
        private readonly ScheduleContext _context;

        public RoleController(ScheduleContext context)
        {
            _context = context;
        }

        // GET api/roles
        [HttpGet]
        [Authorize]
        public ActionResult<IEnumerable<Role>> Get()
        {
            var roles = _context.Rollen.OrderBy(m => m.Naam);
            return roles.ToList();
        }
        [Authorize]
        private Role GetRole(int id)
        {
            return _context.Rollen.FirstOrDefault(a => a.RoleID == id);
        }

        // GET api/roles/5
        [HttpGet("{id}")]
        [Authorize]
        public ActionResult<Role> Get(int id)
        {
            return GetRole(id);
        }

        // POST api/roles
        [HttpPost]
        [Authorize]
        public ActionResult<Role> Post([FromBody] Role value)
        {
            _context.Rollen.Add(value);
            _context.SaveChanges();
            return GetRole(value.RoleID);
        }

        // PUT api/roles/5
        [HttpPut("{id}")]
        [Authorize]
        public ActionResult<Role> Put(int id, [FromBody] Role value)
        {
            var role = _context.Rollen.SingleOrDefault(a => a.RoleID == id);
            role.Naam = value.Naam;
            _context.SaveChanges();
            return GetRole(role.RoleID);
        }

        // DELETE api/roles/5
        [HttpDelete("{id}")]
        [Authorize]
        public ActionResult<Role> Delete(int id)
        {
            var role = _context.Rollen.SingleOrDefault(a => a.RoleID == id);
            _context.Rollen.Remove(role);
            _context.SaveChanges();
            return role;
        }
    }
}
