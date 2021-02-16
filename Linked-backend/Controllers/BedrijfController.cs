using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Linked_backend.Data;
using Linked_backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Linked_backend.Controllers
{
    [Route("api/bedrijven")]
    [ApiController]
    public class BedrijfController : Controller
    {
        private readonly ScheduleContext _context;

        public BedrijfController(ScheduleContext context)
        {
            _context = context;
        }

        // GET api/bedrijven
        [HttpGet]
        [Authorize]
        public ActionResult<IEnumerable<Bedrijf>> Get()
        {
            var bedrijven = _context.Bedrijven.OrderBy(m => m.Naam);
            return bedrijven.ToList();
        }
        [Authorize]
        private Bedrijf GetBedrijf(int id)
        {
            return _context.Bedrijven.FirstOrDefault(a => a.BedrijfID == id);
        }

        // GET api/bedrijven/5
        [HttpGet("{id}")]
        [Authorize]
        public ActionResult<Bedrijf> Get(int id)
        {
            return GetBedrijf(id);
        }

        // POST api/bedrijven
        [HttpPost]
        [Authorize]
        public ActionResult<Bedrijf> Post([FromBody] Bedrijf value)
        {
            _context.Bedrijven.Add(value);
            _context.SaveChanges();
            return GetBedrijf(value.BedrijfID);
        }

        // PUT api/bedrijven/5
        [HttpPut("{id}")]
        [Authorize]
        public ActionResult<Bedrijf> Put(int id, [FromBody] Bedrijf value)
        {
            var bedrijf = _context.Bedrijven.SingleOrDefault(a => a.BedrijfID == id);
            bedrijf.Naam = value.Naam;
            bedrijf.Adres = value.Adres;
            bedrijf.BTWNummer = value.BTWNummer;
            bedrijf.Email = value.Email;
            _context.SaveChanges();
            return GetBedrijf(bedrijf.BedrijfID);
        }

        // DELETE api/bedrijven/5
        [HttpDelete("{id}")]
        [Authorize]
        public ActionResult<Bedrijf> Delete(int id)
        {
            var bedrijf = _context.Bedrijven.SingleOrDefault(a => a.BedrijfID == id);
            _context.Bedrijven.Remove(bedrijf);
            _context.SaveChanges();
            return bedrijf;
        }
    }
}
