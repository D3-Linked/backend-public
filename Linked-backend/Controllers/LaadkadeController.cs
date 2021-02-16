using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Linked_backend.Data;
using Linked_backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Linked_backend.Controllers
{
    [Route("api/laadkades")]
    [ApiController]
    public class LaadkadeController : Controller
    {
        private readonly ScheduleContext _context;

        public LaadkadeController(ScheduleContext context)
        {
            _context = context;
        }

        // GET api/laadkades
        [HttpGet]
        [Authorize]
        public ActionResult<IEnumerable<Laadkade>> Get()
        {
            var laadkades = _context.Laadkades.OrderBy(m => m.Nummer);
            return laadkades.ToList();
        }
        [Authorize]
        private Laadkade GetLaadkade(int id)
        {
            return _context.Laadkades.FirstOrDefault(a => a.LaadkadeID == id);
        }

        // GET api/laadkades/5
        [HttpGet("{id}")]
        public ActionResult<Laadkade> Get(int id)
        {
            return GetLaadkade(id);
        }
   
        // POST api/laadkades
        [HttpPost]
        [Authorize]
        public ActionResult<Laadkade> Post([FromBody] Laadkade value)
        {
            _context.Laadkades.Add(value);
            _context.SaveChanges();
            return GetLaadkade(value.LaadkadeID);
        }

        // PUT api/laadkades/5
        [HttpPut("{id}")]
        public ActionResult<Laadkade> Put(int id, [FromBody] Laadkade value)
        {
            var laadkade = _context.Laadkades.SingleOrDefault(a => a.LaadkadeID == id);
            laadkade.Nummer = value.Nummer;
            laadkade.Locatie = value.Locatie;
            laadkade.IsBezet = value.IsBezet;
            _context.SaveChanges();
            return GetLaadkade(laadkade.LaadkadeID);
        }

        // DELETE api/laadkades/5
        [HttpDelete("{id}")]
        [Authorize]
        public ActionResult<Laadkade> Delete(int id)
        {
            var laadkade = _context.Laadkades.SingleOrDefault(a => a.LaadkadeID == id);
            _context.Laadkades.Remove(laadkade);
            _context.SaveChanges();
            return laadkade;
        }
    }
}
