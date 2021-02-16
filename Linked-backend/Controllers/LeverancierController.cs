using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Linked_backend.Data;
using Linked_backend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Linked_backend.Controllers
{
    [Route("api/leveranciers")]
    [ApiController]
    
    public class LeverancierController : Controller
    {
        private readonly ScheduleContext _context;

        public LeverancierController(ScheduleContext context)
        {
            _context = context;
        }

        // GET api/leveranciers
        [HttpGet]
        [Authorize]
        public ActionResult<IEnumerable<Leverancier>> Get()
        {
            var leveranciers = _context.Leveranciers.OrderBy(m => m.Code);
            return leveranciers
                .Include(l => l.Bedrijf)
                .ToList();
        }
        [Authorize]
        private Leverancier GetLeverancier(int id)
        {
            return _context.Leveranciers
                .Include(l => l.Bedrijf)
                .FirstOrDefault(a => a.LeverancierID == id);
        }

        // GET api/leveranciers/5
        [HttpGet("{id}")]
        [Authorize]
        public ActionResult<Leverancier> Get(int id)
        {
            return GetLeverancier(id);
        }


        // POST api/leveranciers
        [HttpPost]
        [Authorize]
        public ActionResult<Leverancier> Post([FromBody] Leverancier value)
        {
            _context.Leveranciers.Add(value);
            _context.SaveChanges();
            return GetLeverancier(value.LeverancierID);
        }

        // PUT api/leveranciers/5
        [HttpPut("{id}")]
        [Authorize]
        public ActionResult<Leverancier> Put(int id, [FromBody] Leverancier value)
        {
            var leverancier = _context.Leveranciers.SingleOrDefault(a => a.LeverancierID == id);
            leverancier.Bedrijf = value.Bedrijf;
            leverancier.BedrijfID = value.BedrijfID;
            leverancier.Code = value.Code;
            leverancier.Nummerplaat = value.Nummerplaat;
            _context.SaveChanges();
            return GetLeverancier(leverancier.LeverancierID);
        }

        // DELETE api/leveranciers/5
        [HttpDelete("{id}")]
        [Authorize]
        public ActionResult<Leverancier> Delete(int id)
        {
            var leverancier = _context.Leveranciers.SingleOrDefault(a => a.LeverancierID == id);
            _context.Leveranciers.Remove(leverancier);
            _context.SaveChanges();
            return leverancier;
        }
    }
}
